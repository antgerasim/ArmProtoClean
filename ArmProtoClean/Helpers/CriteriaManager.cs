using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArmProtoClean.Model;

namespace ArmProtoClean.Helpers
{

    public class CriteriaManager
    {

        //private CardCriterion _cardCriterion;
        private readonly IEnumerable _dict;

        public CriteriaManager(IEnumerable dict) { _dict = dict; }

        //Map Button kV to Data-Transfer-Object (DTO) with logic. 
        public CardCriterion CriteriaMaker()
        {
            //Where key=(hardcoded)"ButtonName", Value=Button.CardEdition
            var cardCriterion = new CardCriterion();

            foreach (KeyValuePair<string, object> entry in _dict)
            {
                if (entry.Key.Equals("cbTerritory"))
                {
                    cardCriterion.Territory = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();
                    //Button.CardEdition
                }
                if (entry.Key.Equals("cbKindNpa"))
                {
                    cardCriterion.CardKind = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();
                    //Ternary without else
                }
                if (entry.Key.Equals("cbAdoptionKindSubject"))
                {
                    cardCriterion.CardAdoptionKindSubject = ((string) entry.Value == "Все")
                        ? ""
                        : entry.Value.ToString();
                }
                if (entry.Key.Equals("cbAdoptionSubject"))
                    cardCriterion.CardAdoptionSubject = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();
                if (entry.Key.Equals("tbFindAll")) //[tbFindAll,
                {
                    //_cardCriterion.CardName = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();SHIT!
                    cardCriterion.FindAll = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();
                }
                if (entry.Key.Equals("tbCardAdoptionNumber")) //[tbFindAll,
                {
                    //_cardCriterion.CardName = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();SHIT!
                    cardCriterion.CardAdoptionNumber = ((string) entry.Value == "Все") ? "" : entry.Value.ToString();
                }
                //if (entry.Key.Equals("tbCardEdition"))
                //{
                //    _cardCriterion.CardEdition = ((string) entry.Value == "Все")
                //        ? ""
                //        : entry.Value.ToString();
                //}
                if (entry.Key.Equals("dtpMin"))
                {
                    //_cardCriterion.CardAdoptionDateMin = (DateTime) entry.Value;//dont work?
                    cardCriterion.CardAdoptionDateMin = (DateTime) entry.Value;
                }
                if (entry.Key.Equals("dtpMax"))
                    cardCriterion.CardAdoptionDateMax = (DateTime) entry.Value;
            }
            return cardCriterion;
        }

        public List<KeyValuePair<string, object>> GetSearchCriterion()
        {
            List<KeyValuePair<string, object>> mappedcList = new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("CardKind", CriteriaMaker().CardKind),
                new KeyValuePair<string, object>("CardAdoptionKindSubject", CriteriaMaker().CardAdoptionKindSubject),
                new KeyValuePair<string, object>("Territory", CriteriaMaker().Territory),
                new KeyValuePair<string, object>("FindAll", CriteriaMaker().FindAll),
                new KeyValuePair<string, object>("CardAdoptioNumber", CriteriaMaker().CardAdoptionNumber),
                ////new KeyValuePair<string, object>("CardEdition", cC.CardEdition),
                new KeyValuePair<string, object>("CardAdoptionDateMin", CriteriaMaker().CardAdoptionDateMin),
                new KeyValuePair<string, object>("CardAdoptionDateMax", CriteriaMaker().CardAdoptionDateMax)
            };

            //works!! No Collection error! Goldig:)
            //http://stackoverflow.com/a/1637448

            foreach (KeyValuePair<string, object> pair in
                mappedcList.Where(kvp => string.IsNullOrEmpty(kvp.Value.ToString())).ToList())
                mappedcList.Remove(pair);

            return mappedcList;
        }

    }

}