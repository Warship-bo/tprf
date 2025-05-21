sc create TPRFReleaseService binpath= "E:\TestWeb\TPRF_Ajax\ReleaseService\20151222\TPRFReleaseService.exe" start= auto
sc description TPRFReleaseService "TPRF Auto Release Service"
sc start TPRFReleaseService