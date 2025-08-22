using MAUIBLAZORHYBRID.Services.Deactivate;
using MAUIBLAZORHYBRID.Services.Interfaces;
using MAUIBLAZORHYBRID.Services.Sync;
using MAUIBLAZORHYBRID.Services.Upload;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace MAUIBLAZORHYBRID.Services
{
        public class BackgroundDataService : IDisposable
        {
            private readonly IDataUploadService _uploadService;
            private readonly ISyncService _syncService;

            private readonly ILogger<BackgroundDataService> _logger;

            private readonly IPreferences _preferences;
            private PeriodicTimer _uploadTimer;
            private CancellationTokenSource _cts;
            private readonly SemaphoreSlim _syncLock = new(1, 1);
            private readonly IDeviceStatusService _deviceStatus;


            //public bool IsInitialSyncComplete { get; private set; }
            public event Action InitialSyncCompleted;
            public event Action<Exception> InitialSyncFailed;

            //private const string SyncStateKey = "InitialSyncCompleteV0.2.3";

            private readonly SemaphoreSlim _initLock = new(1, 1);
            private bool _initialSyncStarted = false;
        private readonly DeviceStateService _deviceState;
        public BackgroundDataService(
                IDataUploadService uploadService,
                ISyncService syncService,
                ILogger<BackgroundDataService> logger,
                IPreferences preferences,
                 IDeviceStatusService deviceStatus,
                 DeviceStateService deviceState)
            {
                _uploadService = uploadService;
                _syncService = syncService;
                _logger = logger;
                _preferences = preferences;

            _deviceStatus = deviceStatus;
              _deviceState = deviceState;

            // Load initial sync state
            //IsInitialSyncComplete = _preferences.Get(SyncStateKey, false);
            }

        public async Task EnsureInitialSyncAsync()
        {
            //if (IsInitialSyncComplete || _initialSyncStarted)
            //    return;

            await _initLock.WaitAsync();
            try
            {
                //if (IsInitialSyncComplete || _initialSyncStarted)
                //    return;

                _initialSyncStarted = true;
                await PerformInitialSyncAsync(_cts.Token);
            }
            finally
            {
                _initLock.Release();
            }
        }

        public void StartBackgroundTasks()
            {
            try
            {
                if (_cts != null && !_cts.IsCancellationRequested)
                    return;

                _cts = new CancellationTokenSource();

                // Start upload timer (every 5 minutes)
                _uploadTimer = new PeriodicTimer(TimeSpan.FromMinutes(1));
                _ = Task.Run(() => ProcessUploadsAsync(_cts.Token));

                // Perform initial sync if not already done
                    _ = Task.Run(() => PerformInitialSyncAsync(_cts.Token));
                //if (!IsInitialSyncComplete)
                //{
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"BACKGROUND START FAILED: {ex}");
                throw;
            }
        }

            public void StopBackgroundTasks()
            {
                _cts?.Cancel();
                _logger.LogInformation("Background tasks stopped");
            }

            public async Task TriggerManualSyncAsync()
            {
                try
                {
                    _logger.LogInformation("Manual sync triggered");
                    await PerformSyncOperationsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during manual sync");
                    throw;
                }
            }

            private async Task PerformInitialSyncAsync(CancellationToken token)
            {
                //if (IsInitialSyncComplete) return;

                await _syncLock.WaitAsync(token);
                try
                {
                    //if (!IsInitialSyncComplete) // Double-check
                    //{
                        _logger.LogInformation("Performing initial sync...");
                        await PerformSyncOperationsAsync();

                        // Mark as complete
                        //IsInitialSyncComplete = true;
                    //_preferences.Set(SyncStateKey, true);


                    if (InitialSyncCompleted != null)
                    {
                        if (DeviceInfo.Platform == DevicePlatform.Unknown)
                        {
                            InitialSyncCompleted.Invoke();
                        }
                        else
                        {
                            Application.Current.Dispatcher.Dispatch(() =>
                            {
                                InitialSyncCompleted.Invoke();
                            });
                        }
                    }



                    _logger.LogInformation("Initial sync completed");
                    //}
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during initial sync");
                    InitialSyncFailed?.Invoke(ex);
                    throw;
                }
                finally
                {
                    _syncLock.Release();
                }
            }

            private async Task PerformSyncOperationsAsync()
            {
                if (!await _deviceStatus.IsDeviceActiveAsync())
                {
                    _logger.LogWarning("Device deactivated. Sync aborted.");
                    _deviceState.SetDeviceState(false);
                    return;
                }
                await _syncService.SyncBranchesWithMasters();
                await _syncService.SyncOtherMasters();
                await _syncService.SyncItemData();
                await _syncService.SyncItemParentChildData();
                await _syncService.SyncBarItemCounterStock();
                await _syncService.SyncBarItemGodownStock();
        }

            private async Task ProcessUploadsAsync(CancellationToken token)
            {
                try
                {
                    while (await _uploadTimer.WaitForNextTickAsync(token))
                    {


                        if (!await _deviceStatus.IsDeviceActiveAsync(token))
                        {
                            _logger.LogWarning("Device deactivated. Uploads aborted.");
                            _deviceState.SetDeviceState(false);
                            continue;
                        }

                        _logger.LogDebug("Checking for pending uploads...");

                    
                       if (await _uploadService.HasPendingUploadsKOTAsync())
                        { 
                            await _uploadService.UploadPendingKOTsAsync();
                        }
                        if (await _uploadService.HasPendingUploadsBillsAsync())
                        {
                            await _uploadService.UploadPendingDataAsync();
                        }

                        if (await _uploadService.HasPendingUploadsStockTransferAsync())
                        {
                            await _uploadService.UploadPendingStockTransfersAsync();
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Upload processing stopped");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in upload processing");
                }
            }


        public async Task QueueKOTUploadAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (!await _deviceStatus.IsDeviceActiveAsync())
                    {
                        _logger.LogWarning("Device deactivated. KOT upload aborted.");
                        _deviceState.SetDeviceState(false);
                        return;
                    }

                    _logger.LogInformation("Queueing immediate KOT upload...");
                    await _uploadService.UploadPendingKOTsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in immediate KOT upload");
                }
            });
        }

        public async Task QueueBillUploadAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (!await _deviceStatus.IsDeviceActiveAsync())
                    {
                        _logger.LogWarning("Device deactivated. Bill upload aborted.");
                        _deviceState.SetDeviceState(false);
                        return;
                    }

                    _logger.LogInformation("Queueing immediate bill upload...");
                    await _uploadService.UploadPendingDataAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in immediate bill upload");
                }
            });
        }

        public async Task QueueStockTransferUploadAsync()
        {
            await Task.Run(async () =>
            {
                try
                {
                    if (!await _deviceStatus.IsDeviceActiveAsync())
                    {
                        _logger.LogWarning("Device deactivated. Stock transfer upload aborted.");
                        _deviceState.SetDeviceState(false);
                        return;
                    }

                    _logger.LogInformation("Queueing immediate stock transfer upload...");
                    await _uploadService.UploadPendingStockTransfersAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in immediate stock transfer upload");
                }
            });
        }
        public void Dispose()
            {
                _cts?.Dispose();
                _syncLock?.Dispose();
                _uploadTimer?.Dispose();
            }
        }
}