Como funciona SmartUP.
Hay tres formas de conexion desde o hacia el servidor.

Se recomienda instalar todas la interfaces en un mismo equipo.
Los archivos configSmartUp_*****.txt debeb siempre estar en C:\SmartUp
FTP: - Lo archivos que SmartUp_****Generic genera se graban en C:\SmartUp\ftpfiles
	 - Los archivos en el folde ftpfiles se suben a la nuve en n minutos configurables.	 

1) Sockets
	- El servidor(PC/Mismo equipo Analito) escucha al SmartUp y le envia informacion del equipo Analito.
	El exe que se debe instalar es SmartUP_SocketGeneric.application
	El Archivo de configuracion a usar es configSmartUp_SocketGeneric.txt
2) Networt Port
    - El servidor(PC/Mismo equipo Analito) envia al Smartup, a un puerto especifico, la informacion del equipo Analito.
	El exe que se debe instalar es SmartUP_NetworkGeneric.application
	El Archivo de configuracion a usar es configSmartUp_NetworkGeneric.txt
3) Serial/ COM
	- El servidor(PC/Mismo equipo Analito) envia por un puerto COM especifo la inforamcion del equipo Analito.
	El exe que se debe instalar es SmartUP_SerialGeneric.application
	El Archivo de configuracion a usar es  configSmartUp_SerialGeneric.txt

