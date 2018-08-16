# PUREWIT

Desarrollo de plataforma virtual para el lanzamiento de mensajes SMS y llamadas desde la web.

## Objetivos

Implementar una plataforma web que permita la comunicación directa con usuarios mediante uso de mensajes de texto y llamadas utilizando
conexiones móviles.

## Pre - Requisitos

### Ngrok
- Descargue la aplicación [ngrok](https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-windows-amd64.zip).
- Visite la ruta donde descargo la aplicación y descomprimala.
- Inicie la aplicación (.exe) y escriba el siguiente comando:
```
$ ngrok.exe http [port] -host-header="localhost:[port]"
```
- Revise que la información que se muestra sea de la siguiente manera, y que la conexión corresponde a la ruta donde se está ejecutando su aplicación:
<div align="center">
  <img src="https://docs.kentico.com/download/attachments/72976069/image2017-3-30_15-52-14.png?version=1&modificationDate=1490881934994&api=v2"><br><br>
</div>

- Utilice la ruta de Forwarding que corresponde a HTTP no HTTPS.
- Cambie en WiserSoft.UI/Controllers/DifusionController.cs la línea número 30, de la siguiente manera:
```
public string link = "http://[identificador].ngrok.io";
```
### Twilio
- Ingrese a [Twilio](https://www.twilio.com/).
- [Registrese](https://www.twilio.com/try-twilio) para que pueda tener una cuenta gratuita.
- [Obtenga](https://www.twilio.com/console/phone-numbers/incoming) un número de Twilio para poder asignarlo a su usuario.
- Una vez que se obtiene el número de click sobre el mismo.
<div align="center">
  <img src="https://s3.amazonaws.com/com.twilio.prod.twilio-docs/images/phone_number_list.width-800.jpg"><br><br>
</div>

- En la sección de "Messaging" y luego "A message comes in" debe de cambiar el link por default que tiene Twilio y usar el generado con ngrok:
```
http://[identificador].ngrok.io/Sms;
```
<div align="center">
  <img src="https://s3.amazonaws.com/www.appcelerator.com.images/twilio_1.png"><br><br>
</div>

- Por ser una versión de prueba, usted deberá [verificar](https://www.twilio.com/console/phone-numbers/verified) los números a los que desea mandar mensajes. 

## Construir con:
- Visual estudio
```
- Visual Studio 2015.
- Visual Studio 2016.
```
- MySql
```
- MySQL 5.0
- MySQL 5.1
- MySQL 5.2
- MySQL 5.3
```
## Autores

* **Priscilla Bravo** - *Desarrollador de software* 
* **Fernanda Fernandez** - *Desarrollador de software* 
* **Juan Chanto** - *Desarrollador de software* 
* **Brandon Madriz** - *Desarrollador de software* 

