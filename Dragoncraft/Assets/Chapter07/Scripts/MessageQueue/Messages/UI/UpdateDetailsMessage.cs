using System.Collections.Generic;
using UnityEngine;

namespace Dragoncraft
{
    public class UpdateDetailsMessage : IMessage
    {
        public List<UnitComponent> Units;
        public GameObject Model;
    }
}
