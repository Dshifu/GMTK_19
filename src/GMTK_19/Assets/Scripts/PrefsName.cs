using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsName : MonoBehaviour
{
    public const string ReducerPanic = "ReducerPanic";
    public const string VelocityPanic = "VelocityPanic";
    public const string MovementBonus = "MovementBonus";
    public const string LowPanicVolume = "LowPanicVolume";
    public const string MediumPanicVolume = "MediumPanicVolume";
    public const string HighPanicVolume = "HighPanicVolume";
    public const string MasterVolume = "MasterVolume";


    public class AnimatorState
    {
        public const string Move = "Move";
        public const string FireShit = "FireShit";
        public const string Goroh = "Goroh";
        public const string Coka = "Coka";
        public const string Shampoo = "Shampoo";
        public const string AK = "AK";
        public const string IsNeedToPlayParticle = "IsNeedToPlayParticle";
        public const string MoveRight = "MoveRight";
        public const string MoveLeft = "MoveLeft";
        public const string HeartRate = "HeartRate";
        public const string StartOpening = "StartOpening";
        public const string StartFading = "StartFading";
        public const string StartUnFading = "StartUnFading";
    }
}
