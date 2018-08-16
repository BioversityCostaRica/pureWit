# PUREWIT

Desarrollo de plataforma virtual para el lanzamiento de mensajes SMS y llamadas desde la web.

## Objetivos

Implementar una plataforma web que permita la comunicación directa con usuarios mediante uso de mensajes de texto y llamadas utilizando
conexiones móviles.

## Empezando

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
  <img src="https://www.google.com/url?sa=i&rct=j&q=&esrc=s&source=images&cd=&cad=rja&uact=8&ved=2ahUKEwiZ-pPEyvDcAhVlzlkKHTCAASQQjRx6BAgBEAU&url=%2Furl%3Fsa%3Di%26rct%3Dj%26q%3D%26esrc%3Ds%26source%3Dimages%26cd%3D%26cad%3Drja%26uact%3D8%26ved%3D2ahUKEwiZ-pPEyvDcAhVlzlkKHTCAASQQjRx6BAgBEAU%26url%3D%252Furl%253Fsa%253Di%2526rct%253Dj%2526q%253D%2526esrc%253Ds%2526source%253Dimages%2526cd%253D%2526cad%253Drja%2526uact%253D8%2526ved%253D%2526url%253Dhttps%25253A%25252F%25252Fwww.twilio.com%25252Fdocs%25252Fsms%25252Ftutorials%25252Fhow-to-send-sms-messages-c%2526psig%253DAOvVaw1ij3BTfckV5vFbG3moGlrd%2526ust%253D1534474862093251%26psig%3DAOvVaw1ij3BTfckV5vFbG3moGlrd%26ust%3D1534474862093251&psig=AOvVaw1ij3BTfckV5vFbG3moGlrd&ust=1534474862093251"><br><br>
</div>

- En la sección de "Messaging" y luego "A message comes in" debe de cambiar el link por default que tiene Twilio y usar el generado con ngrok:
```
http://[identificador].ngrok.io/Sms;
```
<div align="center">
  <img src="https://s3.amazonaws.com/www.appcelerator.com.images/twilio_1.png"><br><br>
</div>

- Por ser una versión de prueba, usted deberá [verificar](https://www.twilio.com/console/phone-numbers/verified) los números a los que desea mandar mensajes. 


## Autores

* **Priscilla Bravo** - *Desarrollador de software* 
* **Fernanda Fernandez** - *Desarrollador de software* 
* **Juan Chanto** - *Desarrollador de software* 
* **Brandon Madriz** - *Desarrollador de software* 

