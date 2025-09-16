%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe %~dp0\DINEUsbWinService.exe
Net Start EUsbKeyService
sc config EUsbKeyService start= auto
pause