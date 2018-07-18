# pureWit
----------------------------------------------------------------------------------------------------------------------------------------
# Descripción
Desarrollo de plataforma virtual para el lanzamiento de mensajes SMS y llamadas desde la web.

# Objetivos
Implementar una plataforma web que permita la comunicación directa con usuarios mediante uso de mensajes de texto y llamadas utilizando
conexiones móviles.

Empezando
---------------

- Descargue la aplicación ngrok para windows, utilizando el siguiente link:
```
https://bin.equinox.io/c/4VmDzA7iaHb/ngrok-stable-windows-amd64.zip
```
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

- Ejecute la aplicación con visual studio 2015 o 2017.

# Autores
Priscilla Bravo <br>
Fernanda Fernandez <br>
Juan Chanto <br>
Brandon Madriz <br>
