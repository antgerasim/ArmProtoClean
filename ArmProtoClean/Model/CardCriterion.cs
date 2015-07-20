using System;
using System.Collections.Generic;
using System.Text;

namespace ArmProtoClean.Model
{

    public class CardCriterion
    {

        public CardCriterion() { }

        public CardCriterion(UInt32 editionId, UInt32 cardId, string territory, string cardKind, string cardName,
            string cardEdition, string cardAdoptionKindSubject, string cardAdoptionSubject, string cardAdoptionNumber,
            DateTime cardAdoptionDate)
        {

            EditionId = editionId;
            CardId = cardId;
            Territory = territory;
            CardKind = cardKind;
            CardName = cardName;
            CardEdition = cardEdition;
            CardAdoptionKindSubject = cardAdoptionKindSubject;
            CardAdoptionSubject = cardAdoptionSubject;
            CardAdoptionNumber = cardAdoptionNumber;
            CardAdoptionDate = cardAdoptionDate;
        }

        public UInt32 EditionId { get; internal set; }

        public UInt32 CardId { get; internal set; }

        public string Territory { get; internal set; }

        public string CardKind { get; internal set; }

        public string CardName { get; internal set; } //FullText, use Queryparser

        public string CardEdition { get; set; } //FullText, use Queryparser

        public string FileExt { get; set; }

        public string CardAdoptionKindSubject { get; internal set; }

        public string CardAdoptionSubject { get; internal set; }

        public DateTime CardAdoptionDate { get; set; }

        public string FindAll { get; set; } //Single line 

        public string CardAdoptionNumber { get; set; }

        //use to index the cardadoptiondate coloumn in DB

        public DateTime CardAdoptionDateMin { get; set; }

        //use for range searching the CardAdoptionDate field

        public DateTime CardAdoptionDateMax { get; set; }

        //use for range searching the CardAdoptionDate field

        //public string ContentType { get; set; }
        /// <summary>
        ///     MIME Content-Type of the data from which the text was extracted.
        /// </summary>
        /// <summary>
        ///     Dictionary of meta data (e.g. titie, size, dimensions) about the data source of the extraction.
        /// </summary>
        public IDictionary<string, string> ContentMetadataDict { get; set; }

        /// <summary>
        ///     Pretty print the output of the extraction
        /// </summary>
        /// <returns>Human readable version of the text extraction result</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("CardEdition:\n" + CardEdition + "MetaData:\n");

            foreach (var keypair in ContentMetadataDict)
                builder.AppendFormat("{0} - {1}\n", keypair.Key, keypair.Value);
            return builder.ToString();
        }

    }

}