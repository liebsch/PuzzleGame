// Control Direction Grab Action|SecondaryControllerGrabActions|60040
namespace VRTK.SecondaryControllerGrabActions
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// Controls the facing direction of the grabbed Interactable Object to rotate in the direction of the secondary grabbing object.
    /// </summary>
    /// <remarks>
    ///   > Rotation will only occur correctly if the Interactable Object `forward` is correctly aligned to the world `z-axis` and the `up` is correctly aligned to the world `y-axis`. It is also not possible to control the direction of an Interactable Object that uses the Joint based grab mechanics.
    ///
    /// **Script Usage:**
    ///  * Place the `VRTK_ControlDirectionGrabAction` script on either:
    ///    * The GameObject of the Interactable Object to detect interactions on.
    ///    * Any other scene GameObject and then link that GameObject to the Interactable Objects `Secondary Grab Action Script` parameter to denote use of the secondary grab action.
    /// </remarks>
    /// <example>
    /// `VRTK/Examples/043_Controller_SecondaryControllerActions` demonstrates the ability to grab an object with one controller and control their direction with the second controller.
    /// </example>
    [AddComponentMenu("VRTK/Scripts/Interactions/Interactables/Secondary Controller Grab Actions/VRTK_ControlDirectionGrabAction")]
    public class ObjectLogic : VRTK_BaseGrabAction
    {

        [Header("Direction Settings")]

        [Tooltip("Prevent the secondary controller rotating the grabbed object through it's z-axis.")]
        //public bool lockZRotation = true;

        protected Vector3 initialPosition;
        protected Quaternion initialRotation;
        protected Quaternion releaseRotation;

        protected float initialLength;
        protected float initialScaleFactor;
        protected Vector3 initialGrabbingPostion;
        protected Vector3 initialForward;
        protected Vector3 initialObjForward;
        protected Vector3 initialControllerObjVector;

        protected Quaternion initialDirection;


        /// <summary>
        /// The Initalise method is used to set up the state of the secondary action when the object is initially grabbed by a secondary controller.
        /// </summary>
        /// <param name="currentGrabbdObject">The Interactable Object script for the object currently being grabbed by the primary grabbing object.</param>
        /// <param name="currentPrimaryGrabbingObject">The Interact Grab script for the object that is associated with the primary grabbing object.</param>
        /// <param name="currentSecondaryGrabbingObject">The Interact Grab script for the object that is associated with the secondary grabbing object.</param>
        /// <param name="primaryGrabPoint">The point on the Interactable Object where the primary Interact Grab initially grabbed the Interactable Object.</param>
        /// <param name="secondaryGrabPoint">The point on the Interactable Object where the secondary Interact Grab initially grabbed the Interactable Object.</param>
        public override void Initialise(VRTK_InteractableObject currentGrabbdObject, VRTK_InteractGrab currentPrimaryGrabbingObject, VRTK_InteractGrab currentSecondaryGrabbingObject, Transform primaryGrabPoint, Transform secondaryGrabPoint)
        {
            base.Initialise(currentGrabbdObject, currentPrimaryGrabbingObject, currentSecondaryGrabbingObject, primaryGrabPoint, secondaryGrabPoint);
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
            initialObjForward = transform.TransformDirection(Vector3.forward);
            
            initialLength = (primaryGrabbingObject.transform.position - secondaryGrabbingObject.transform.position).magnitude;
            initialGrabbingPostion = primaryGrabbingObject.transform.position - secondaryGrabbingObject.transform.position;
            initialScaleFactor = currentGrabbdObject.transform.localScale.x;
            initialForward = secondaryGrabbingObject.transform.TransformDirection(Vector3.forward);
            
            initialControllerObjVector = (grabbedObject.transform.position-primaryGrabbingObject.controllerAttachPoint.position);

            initialDirection = initialRotation * Quaternion.Inverse(Quaternion.LookRotation(primaryGrabbingObject.controllerAttachPoint.position - secondaryGrabbingObject.controllerAttachPoint.position));

           
        }

        /// <summary>
        /// The ResetAction method is used to reset the secondary action when the Interactable Object is no longer grabbed by a secondary Interact Grab.
        /// </summary>
        public override void ResetAction()
        {
            releaseRotation = transform.localRotation;
            if (!grabbedObject.grabAttachMechanicScript.precisionGrab)
            {
                transform.localRotation = initialRotation;
            }
            base.ResetAction();
        }

        /// <summary>
        /// The OnDropAction method is executed when the current grabbed Interactable Object is dropped and can be used up to clean up any secondary grab actions.
        /// </summary>
        public override void OnDropAction()
        {
            base.OnDropAction();
        }

        /// <summary>
        /// The ProcessUpdate method runs in every Update on the Interactable Object whilst it is being grabbed by a secondary Interact Grab.
        /// </summary>
        public override void ProcessUpdate()
        {
            base.ProcessUpdate();
        }

        /// <summary>
        /// The ProcessFixedUpdate method runs in every FixedUpdate on the Interactable Object whilst it is being grabbed by a secondary Interact Grab and influences the rotation of the Interactable Object.
        /// </summary>
        public override void ProcessFixedUpdate()
        {
            base.ProcessFixedUpdate();
            if (initialised)
            {
                RotateObject();
                ScaleObject();
            }
        }

        protected virtual void ApplyScale(float newScale)
        {
            if (newScale > 0.05f && newScale < 2.0f)
            { 
                transform.localScale = new Vector3(newScale, newScale, newScale);
              //  grabbedObject.transform.position = grabbedObject.transform.position - (initialControllerObjVector - (grabbedObject.transform.position - primaryGrabbingObject.controllerAttachPoint.position));
            }
        }

        protected virtual void ScaleObject()
        {
            float currentLength = (primaryGrabbingObject.transform.position - secondaryGrabbingObject.transform.position).magnitude;
            ApplyScale((currentLength - initialLength) + grabbedObject.transform.localScale.x);
            initialLength = currentLength;
        }

        protected virtual void RotateObject()
        {
            Vector3 priAttachPoint = primaryGrabbingObject.controllerAttachPoint.position;
            Vector3 secAttachPoint = secondaryGrabbingObject.controllerAttachPoint.position;            
            Vector3 currentForward = secondaryGrabbingObject.transform.TransformDirection(Vector3.forward);

            transform.position = (priAttachPoint + secAttachPoint)/2;

/*
            VRTK_Logger.Debug("initialObjForward: " + initialObjForward);
            VRTK_Logger.Debug("initialForward: " + initialForward);
*/
            VRTK_Logger.Debug("initialRotation: " + initialRotation);
            VRTK_Logger.Debug("transform.rotation: " + transform.rotation);
            VRTK_Logger.Debug("_________________________");

//TODO currentForward mit den richtigen Ausrichtung ersetzen


            //transform.rotation = Quaternion.LookRotation(priAttachPoint - secAttachPoint); //, currentForward);
            
//            transform.rotation = transform.rotation; //  Quaternion.LookRotation(transform.TransformDirection(Vector3.forward)); // + (priAttachPoint - secAttachPoint), currentForward); // initialDirection + currentForward);
            

            //obj.transform.rotation = controllerAttachPoint.transform.rotation * Quaternion.Inverse(grabbedSnapHandle.transform.localRotation);

//            transform.rotation = Quaternion.LookRotation(secondaryGrabbingObject.transform.position - primaryGrabbingObject.transform.position, secondaryGrabbingObject.transform.TransformDirection(Vector3.forward));
            //transform.rotation = Quaternion.LookRotation(transform.position - primaryGrabbingObject.transform.position); //, transform.TransformDirection(Vector3.forward) + secondaryGrabbingObject.transform.TransformDirection(Vector3.forward));

            //transform.rotation = initialDirection * Quaternion.LookRotation( (priAttachPoint - secAttachPoint), currentForward);

            transform.rotation = Quaternion.FromToRotation(secAttachPoint - priAttachPoint, currentForward);

/*
        Transform lHand = primaryGrabbingObject.transform;
        Transform rHand = secondaryGrabbingObject.transform;

        Vector3 handDir0 = (priAttachPoint - secAttachPoint).normalized;
        Vector3 handDir1 = (rHand.position - lHand.position).normalized;

        Quaternion handrRot = Quaternion.FromToRotation(handDir0, handDir1);

        transform.rotation = handrRot * transform.rotation;
*/          
            //transform.rotation = Quaternion.LookRotation(primaryGrabbingObject.controllerAttachPoint.transform.position - primaryInitialGrabPoint.position, secondaryGrabbingObject.transform.TransformDirection(Vector3.forward));
        }
    }
}
/*
 {
 
        // Get wands
        Transform lHand = PE_PuppetManager.Instance.puppetHands[0].physicalTarget.transform;    //primaryGrabbingObject
        Transform rHand = PE_PuppetManager.Instance.puppetHands[1].physicalTarget.transform;    //secondaryGrabbingObject
 
        // Original hand dir
        Vector3 handDir0 = (rightGrabPos - leftGrabPos).normalized;                             //initial   
 
        // Current hand dir
        Vector3 handDir1 = (rHand.position - lHand.position).normalized;
 
        // Difference rot
        Quaternion handRot = Quaternion.FromToRotation(handDir0, handDir1);
 
        // Apply
        holdable.rigidbody.transform.rotation = handRot * grabbedRot;
    }
   
    // ON EVENTS
    void OnBothHold(Evt_Interactable_OnBothHold e)
    {

        // Get wands
        Transform lHand = PE_PuppetManager.Instance.puppetHands[0].physicalTarget.transform;    //primaryGrabbingObject
        Transform rHand = PE_PuppetManager.Instance.puppetHands[1].physicalTarget.transform;    //secondaryGrabbingObject
 
        // Store grab pos                                                                       
        leftGrabPos = lHand.position;                                                           //primaryGrabPoint
        rightGrabPos = rHand.position;                                                          //secondaryGrabPoint
       
        // And rotation of obj
        grabbedRot = holdable.rigidbody.transform.rotation;                                     //transform.rotation
    }
    */