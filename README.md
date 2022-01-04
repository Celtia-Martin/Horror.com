# Horror.com
 
## Information
 
Horror Game demo in VR.
Language of code, interface and documentation: Spanish

## Introducción

 Este proyecto consiste en una pequeña demo de terror para realidad virtual, más concretamente, diseñado para Oculus Rift S. 
El objetivo del jugador será conseguir la mayor cantidad de puntuación fotografiando al espectro 
que reside en su piso. Pero no puede acercarse demasiado, ya que, si el monstruo decide atacarle, podrá sufrir graves daños.
No se pretende que esta demo sea un juego completo, si no una pequeña demostración y experimentación sobre como se podría implementar este tipo de jugabilidad en Realidad Virtual. Por tanto, se ha hecho énfasis en el tratamiento del sonido y en la implementación de los controles básicos del juego.

## Flujo del juego

 El juego comenzará con el jugador en su habitación, dos manos en la posición de los mandos, y un 
teléfono móvil con la cámara activada en la mano derecha. 

 Las acciones principales que el jugador podrá realizar son moverse, sacar fotos, apagar o encender la linterna, y coger cosas. Esta última mecánica es importante, ya que llegará un momento en el que el móvil se quedará sin batería y el jugador tendrá que cargarlo con un cargador.

 A lo largo de la partida, el monstruo irá apareciendo aleatoriamente en distintos lugares de la casa, haciendo un sonido para que el jugador pueda localizarlo. Si a la hora de hacer fotografías el jugador se queda mucho tiempo delante del monstruo, este le meterá un susto y se le restará una vida. En total, se tienen dos vidas, por lo tanto, la segunda vez que el monstruo le asuste, morirá.

 Al morir, se muestra una pantalla con los resultados y la opción de salir de la demo o de volver a 
jugar.

## Sobre el movimiento

 Entre las diferentes opciones de locomoción para esta demo, se ha escogido el movimiento por medio del joystick y “Tunneling” por su naturaleza. Un método de teletransporte habría sido mucho menos dinámico, y tampoco se podría haber usado la posición real del jugador, ya que normalmente el área de juego será más pequeña que el piso de la demo. 
 
 Habiendo elegido el tipo de movimiento, se decidió implementar un efecto de “Tunneling” para poder evitar, o al menos mitigar, la sensación de Cybersickness.

## Arte

 El monstruo, el cargador, la mascarilla y el móvil, así como los elementos de arte 2D de la interfaz son assets propios.
Todos los demás modelos son assets gratuitos.

## Herramientas externas utilizadas

ResonanceAudioMixer
XR Interaction Toolkit
Oculus XR Plugin
XR Device Simulator

## Notas sobre la ejecución

 Tal y como el proyecto está entregado, se puede ejecutar la demo sin Oculus con XR Device Simulator. Sin embargo, para poder ejecutarla en las Oculus, se deberá desactivar el objeto homónimo, desactivar el collider actual del XR Rig y activar el que está desactivado, y modificar la posición del XR Rig de la siguiente manera: (-3.259, 4.925, 12.62).

