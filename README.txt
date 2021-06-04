------------------------------------------------------
Como funciona SmartUP.
Hay tres formas de conexion desde o hacia el servidor.

Se recomienda instalar todas la interfaces en un mismo equipo.
Los archivos configSmartUp_*****.txt debeb siempre estar en C:\SmartUp o en D:\SmartUp si no tiene unidad C la PC.
FTP: - Lo archivos que SmartUp_****Generic genera se graban en C:\SmartUp\ftpfiles
	 - Los archivos en el folder ftpfiles se suben a la nuve en 'N' minutos configurables.	 

1) Sockets {Form:frmFromSocketGeneric}
	- El servidor(PC/Mismo equipo Analito) escucha al SmartUp y le envia informacion del equipo Analito.
	El exe que se debe instalar es SmartUP_SocketGeneric.application
	El Archivo de configuracion a usar es configSmartUp_SocketGeneric.txt

2) Networt Port {Form:frmFromNetworkGeneric}
    - El servidor(PC/Mismo equipo Analito) envia al Smartup, a un puerto especifico, la informacion del equipo Analito.
	El exe que se debe instalar es SmartUP_NetworkGeneric.application
	El Archivo de configuracion a usar es configSmartUp_NetworkGeneric.txt

3) Serial/COM {Form:frmFromSerialGeneric}
	- El servidor(PC/Mismo equipo Analito) envia por un puerto COM especifo la inforamcion del equipo Analito.
	El exe que se debe instalar es SmartUP_SerialGeneric.application
	El Archivo de configuracion a usar es  configSmartUp_SerialGeneric.txt

------------------------------------------------------
Como ENVIAR los archivos generados a la nuve via FTP.
	SendFTP.xml se debe importal en el Schedule Task de windows.

-----------------------------------------------------
Como hacer deploy de los exes.
No existe un exe para cada tipo de conexion, en cambio debe hacerce un deploy por cada uno.
Ejemplo: Para generar el exe de sockets, se debe nombrar el assembly como SmartUP_SocketGeneric, para que el metodo Main abra la forma frmFromSocketGeneric
e inicialice el exe a partit del archivo de configuracion configSmartUp_SocketGeneric.txt localizado en C:\SmartUp.

