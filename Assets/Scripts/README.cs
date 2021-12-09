/*/Suche in den Scripten nach 

        "//Edit by Laurin"

        bis

        "//End"



__________________________________________________________________
VRTK_InteractTouch.cs   1x
    Puzzle\Assets\VRTK\Source\Scripts\Interactions\Interactors\VRTK_InteractTouch.cs 

    1.  Fehlerausgabe beim Starten der Scene aus -> "RigidBody::setRigidBodyFlag: kinematic bodies with CCD enabled are not supported!"
        Umgestellt von

            touchRigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        
        auf
        
            touchRigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete; 
        

__________________________________________________________________
VRTK_SnapDropZone.cs    2x
    Puzzle\Assets\VRTK\Prefabs\SnapDropZone\VRTK_SnapDropZone.cs 

        VRTK_SnapDropZone wurde zum minimalen skalieren und drehen der Puzzle Teile benutzt.

    1.  Da die Puzzle Teile eine annähernde Größe zu den vorgesehenen SnapDropZones aufweisen soll, wurde mithilfe der if-Abfrage
    
            if((interactableObjectCheck.transform.localScale.x*10)+0.1f > transform.localScale.x && (interactableObjectCheck.transform.localScale.x*10)-0.1f < transform.localScale.x)

        das Einsetzen der Puzzleteile verhindert, wenn die Scale größer oder kleiner als 0.1 zur vorgegebenen Größe der SnapDropZones ist

    
    2.  Das Setzen der Puzzle Teile in die SnapDropZones richtet normalerweise die Puzzle Teile nach dem WorldSpace aus.
        Durch die Abfragen der Rotation des einzusetzenden Puzzle Teiles werden die Puzzle Teile immer auf 0° (mod 90) gesetzt.
        Daher liegen die Puzzle Teile immer "gerade" auf den PuzzlePlates und ergeben am Ende ein zusammenhängendes (relativ) lückenfreies Bild.
          
    3.  Richtige Scale (mit Lerp)

        ioTransform.localScale = Vector3.Lerp(startScale, endSettings.transform.localScale/5, (elapsedTime / duration));

    4.  Falls bei 3. etwas falsch läuft, wird die richtige Scale noch einmal gesetzt  

        ioTransform.localScale = (endSettings.transform.localScale/5);

*/