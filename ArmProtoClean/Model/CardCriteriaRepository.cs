using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ArmProtoClean.Helpers;
using ArmProtoClean.Tika;

namespace ArmProtoClean.Model
{

    public  class CardCriteriaRepository
    {

        public  event EventHandler<ProgressBarEventArgs> ProgressEvent;

        public void Function(int progress, int maximum)
        {
            OnRaiseProgressEvent(new ProgressBarEventArgs(progress, maximum));
            
        }

        private void OnRaiseProgressEvent(ProgressBarEventArgs e)
        {
            EventHandler<ProgressBarEventArgs> handler = ProgressEvent;
            if (handler != null)
            {
                //this is what actually raises the event.
                handler(null, e);
            }
        }

        public CardCriterion Get(int id)
        {
            return GetAll().SingleOrDefault(x => x.EditionId.Equals(id));
            //return GetAll(_ds.)
        }

        
        public  List<CardCriterion> GetAll()
        {
            
            var Ds = new DataSet(); //dummy var to not blow the method
            DataRowCollection rows = Ds.Tables[0].Rows;

            //Map all Database Rows To DataModels. Add each Model to Dict.
            List<CardCriterion> cardCriteria = new List<CardCriterion>(rows.Count);

            #region Linq

            //cardCriteria.AddRange(from DataRow row in Ds.Tables[0].Rows
            //    select row.ItemArray
            //    into values
            //    let textExtractor = new TextExtractor()
            //    select
            //        new CardCriterion()
            //        {
            //            CardId = Convert.ToInt16(values[0]),
            //            Territory = Convert.ToString(values[1]),
            //            CardKind = Convert.ToString(values[2]),
            //            CardAdoptionKindSubject = Convert.ToString(values[3]),
            //            CardAdoptionSubject = Convert.ToString(values[4]),
            //            CardName = Convert.ToString(values[5]),
            //            CardEdition = textExtractor.Extract((byte[]) values[6]).Text,
            //            ContentMetadataDict = textExtractor.Extract((byte[]) values[6]).Metadata,
            //            ContentType = textExtractor.Extract((byte[]) values[6]).ContentType
            //        });

            #endregion

            foreach (DataRow row in rows)
            {
                var criterion = new CardCriterion();
                object[] values = row.ItemArray;
                var textExtractionResult = new TextExtractor().Extract((byte[]) values[7]);

                criterion.EditionId = Convert.ToUInt32(values[0]);
                criterion.Territory = Convert.ToString(values[1]);
                criterion.CardKind = Convert.ToString(values[2]);
                criterion.CardAdoptionKindSubject = Convert.ToString(values[3]);
                criterion.CardAdoptionSubject = Convert.ToString(values[4]);
                criterion.CardName = Convert.ToString(values[5]);
                criterion.CardAdoptionDate = Convert.ToDateTime(values[6]);
                criterion.CardEdition = textExtractionResult.Text;
                criterion.ContentMetadataDict = textExtractionResult.Metadata;
                criterion.CardAdoptionNumber = Convert.ToString(values[8]);

                cardCriteria.Add(criterion);

                Function(1, rows.Count);
                //http://stackoverflow.com/questions/6471378/implementing-a-progress-bar-to-show-work-being-done

            }
            return cardCriteria;
            //http://codereview.stackexchange.com/questions/30714/faster-way-to-convert-datatable-to-list-of-class
        }

    }

}