using UnityEngine;
using HGS.Enums;

namespace HGS.Tools.Services.ServiceSounds {

    public class SoundCaseOnDestroy: MonoBehaviour, ISoundCaseOnDestroy {
        
        [field:SerializeField]
        public SoundCase Sound { get; private set; }

    }

}