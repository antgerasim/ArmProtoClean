using System;
using System.Collections.Generic;

namespace ArmProtoClean.Helpers
{

    public static class DateManager
    {

        private static DateTime _cardAdoptionDateMin;
        private static DateTime _cardAdoptionDateMax;

        public static string GetCardAdoptionDateCase(KeyValuePair<string, object> kvpMin,
            KeyValuePair<string, object> kvpMax)
        {
            _cardAdoptionDateMin = (DateTime) kvpMin.Value;
            _cardAdoptionDateMax = (DateTime) kvpMax.Value;

            if (!_cardAdoptionDateMin.Equals(new DateTime()))
                _cardAdoptionDateMin = (DateTime) kvpMin.Value;

            if (!_cardAdoptionDateMax.Equals(new DateTime()))
                _cardAdoptionDateMax = (DateTime) kvpMin.Value;

            return "CardAdoptionDate";
        }

    }

}