# README - API de Retiros de prueba

Este proyecto es una API desarrollada en C# (.NET 6) que se encarga de manejar los retiros de productos. Además, utiliza la tecnología SignalR para procesar la información a través de una cola de mensajes propia.

## Clonar el repositorio
Para clonar este repositorio, puede ejecutar el siguiente comando en su terminal:



## Ejecutar la API
Para ejecutar la API, debe abrir el proyecto en Visual Studio y ejecutar la aplicación. También puede ejecutarla desde la línea de comandos utilizando el siguiente comando en la carpeta del proyecto:


## Ejecutar el front-end
Para ejecutar el front-end, primero debe asegurarse de que Node.js y NPM estén instalados en su sistema. Luego, desde la carpeta del proyecto, debe ejecutar los siguientes comandos en la terminal:


npm install
npm start

## Usar la API
Para utilizar la API, debe enviar una solicitud POST al endpoint de retiros con un payload en formato JSON. El payload debe contener los siguientes campos:

- `retiros`: un array de códigos de retiro (strings).
- `transportista`: el nombre del transportista (string).
- `auto`: la patente del vehículo de transporte (string).
- `sucursalDestino`: un objeto que contiene el código, nombre y dirección de la sucursal de destino (string).
- `sucursalPlanificacion`: un objeto que contiene el código, nombre y dirección de la sucursal de planificación (string).
- `direccion`: la dirección de la entrega (string).
- `estado`: el estado del retiro (string).
- `motivo`: el motivo del retiro (string).

Aquí hay un ejemplo de payload:

{
"retiros": [
"R123456"
],
"transportista": "Transporte SA",
"auto": "ABC123",
"sucursalDestino": {
"codigo": "S001",
"nombre": "Sucursal 1",
"direccion": "Av. Siempre Viva 123"
},
"sucursalPlanificacion": {
"codigo": "S002",
"nombre": "Sucursal 2",
"direccion": "Calle Falsa 123"
},
"direccion": "Av. Principal 456",
"estado": "Pendiente",
"motivo": ""
}
