using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ArmProtoClean.Helpers;
using ArmProtoClean.Lucene;
using ArmProtoClean.Model;
using ArmProtoClean.Tika;

namespace ArmProtoClean.Sql
{

    public static class SqlManager
    {

        private static string _sqlQuery;
        private static int _rowCount;
        private static bool _shouldClose;
        private static string _databaseName = "ArmMunicipalМО";
        private static string _serverAddress = ".";
        private static string _login = "armUser";
        private static string _password = "fhv123";

        //private static string _connectionString = "";
        //private static string _connectionString= "Data Source=.;initial catalog=ArmMunicipalМО;User Id=armUser;Password=fhv123
        private static string _connectionString = "Data Source=" + _serverAddress + ";" + "initial catalog="
                                                  + _databaseName + ";" + "User Id=" + _login + ";" + "Password="
                                                  + _password;

        public static void FillCredentials(string databaseName, string serverAddress, string login, string password)
        {
            _databaseName = databaseName;
            _serverAddress = serverAddress;
            _login = login;
            _password = password;
            _createConnectionString(_databaseName, _serverAddress, _login, _password);
            _createCountQuery(_databaseName);

        }

        //create connectionstring
        private static void _createConnectionString(string databaseName, string serverAddress, string login,
            string password)
        {
            //sb = @"Data Source=" + serverAddress + ";initial catalog=" + serverName + ";User Id=" + login + ";Password=" + password;
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"Data Source=");
                sb.Append(serverAddress);
                sb.Append(";initial catalog=");
                sb.Append(databaseName);
                sb.Append(";User Id=");
                sb.Append(login);
                sb.Append(";Password=");
                sb.Append(password);

                //server -rumscws169, user-armUser, pass-fhv123
                //"Data Source=RUMSCWS141\\SQLEXPRESS;initial catalog=ArmMunicipal;User Id=armUser;Password=fhv123"
                //"Data Source=RUMSCWS141;initial catalog=ArmMunicipalMO;User Id=armUser;Password=fhv123"
                //"Data Source=.;initial catalog=ArmMunicipalМО;User Id=armUser;Password=fhv123
                _connectionString = sb.ToString();

            }
        }

        private static string _createCountQuery(string databaseName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT COUNT(*) FROM [");
            sb.Append(databaseName);
            sb.Append("].[dbo].[Edition]");
            return sb.ToString();
        }

        public static void CheckRowCount()
        {
            //SqlQuery = _createCountQuery(serverAddress);

            _sqlQuery = @"
SELECT COUNT(*)
FROM   [ArmMunicipalМО].[dbo].[Edition];
--WHERE  fileExt!='.doc' AND CardId>=21977;
--WHERE fileExt!='.doc';
";
            _sqlQuery = _createCountQuery(_databaseName);
            //SqlQuery = "select @@ROWCOUNT";
            _rowCount = _checkRowCount(_sqlQuery);
        }

        private static int _checkRowCount(string sqlQuery)
        {
            //int rowCount = 0;

            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                try
                {
                    _shouldClose = false;
                    sqlCon.Open();
                    SqlCommand sqlCmd = sqlCon.CreateCommand();
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandText = sqlQuery;

                    _rowCount = (int) sqlCmd.ExecuteScalar();
                    _shouldClose = true;
                }
                catch (SqlException sqlException)
                {
                    _shouldClose = true;
                    throw new Exception("SqlException", sqlException);
                }

                finally
                {
                    _shouldClose = true;
                    if (_shouldClose)
                        sqlCon.Close();
                }
            }
            return _rowCount;
        }

        // The query fetch all card details
        //public static DataSet CreateIndex()
        public static void CreateIndex()
        {
            //32612 documents in ArmMunicipal
            //edit to add EditionId
            _sqlQuery = @"
SELECT   Edition.id AS EditionId,
         Card.id AS CardId,
         Territory.name AS Territory,
         CardKind.name AS CardKind,
         Card.name AS CardName,
         Edition.wordDocument AS CardEdition,
         Edition.fileExt AS FileExt,
         SubjectKind.name AS CardAdoptionKindSubject,
         Subject.name AS CardAdoptionSubject,
         Adoption.date AS CardAdoptionDate,
         Adoption.number AS CardAdoptionNumber
FROM     dbo.Card AS Card
         INNER JOIN
         dbo.State AS State
         ON Card.id = State.id
         INNER JOIN
         dbo.Adoption AS Adoption
         ON Card.id = Adoption.id
         INNER JOIN
         dbo.Edition AS Edition
         ON Card.id = Edition.cardId
         INNER JOIN
         dbo.Subject AS Subject
         ON Adoption.subjectId = Subject.id
         INNER JOIN
         dbo.Territory AS Territory
         ON Subject.territoryId = Territory.id
         INNER JOIN
         dbo.Kind AS CardKind
         ON Card.kindId = CardKind.id
         INNER JOIN
         dbo.Kind AS SubjectKind
         ON Subject.kindId = SubjectKind.id
         INNER JOIN
         dbo.Type AS Type
         ON CardKind.typeId = Type.id
WHERE    (State.deleted = 0)
GROUP BY Edition.id, Card.id, Card.name, Card.type, Card.kindId, Territory.name, CardKind.name, SubjectKind.name, 
         Subject.name, Adoption.date, Adoption.number, Edition.wordDocument, Edition.fileExt;
";
            _createIndex(_sqlQuery);
        }

        // Returns the dataset
        //Consider to substitute for resultset for better performance!
        /*"Overall, SqlCeResultSet-based data sources require less memory, have better performance, 
         * but have fewer features than DataSet-based data sources.*/
        //http://msdn.microsoft.com/en-us/library/ms180730%28v=vs.90%29.aspx
        //http://www.lucenetutorial.com/techniques/indexing-databases.html

        private static void _createIndex(string sqlQuery)
        {
            //@"Data Source=RUMSCWS141\SQLEXPRESS;initial catalog=ArmMunicipal;User Id=armUser;Password=fhv123"))
            //ArmMunicipalМО
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                try
                {
                    _shouldClose = false;
                    sqlCon.Open();
                    SqlCommand sqlCmd = sqlCon.CreateCommand();
                    sqlCmd.CommandTimeout = 0;
                    sqlCmd.CommandText = sqlQuery;
                    using (SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        //get data line by line with SqlDataReader
                        while (reader.Read())
                        {
                            try
                            {
                                //1, ru50.501.301,решение,Представительный орган муниципального образования,Совет депутатов сельского поселения Федоскинское,Об утверждении Порядка представления, рассмотрения,внешней проверки, отчета об исполнении бюджета сельского поселения Федоскинское и его утверждения ,26.03.2009 0:00:00
                                Console.WriteLine(_reportStartExtracting(reader));
                                //_reportStartExtracting(reader);
                                LuceneIndex.AddUpdateLuceneIndex(_extractRowToCriterion(reader));
                                Console.WriteLine("-----------EXTRACTING SUCCESSFUL--------------");
                                Function(1, _rowCount); //change from progresbar to write label without maxcount event
                            }
                            catch (TextExtractionException txtExtrEx)
                            {

                                Console.WriteLine(_reportErrorExtracting(reader));
                                var sb = new StringBuilder();

                                #region CatchInnerException

                                try
                                {
                                    sb.Append("ErrorString:@");
                                    sb.Append(_reportErrorExtracting(reader));
                                    sb.Append("@@");
                                    sb.Append("OuterException Message:@");
                                    sb.Append(txtExtrEx.Message);
                                    sb.Append("@@");
                                    if (!string.IsNullOrEmpty(txtExtrEx.InnerException.Message))
                                    {
                                        sb.Append("--InnerException Message:@");
                                        sb.Append("--" + txtExtrEx.InnerException.Message);
                                        sb.Append("@@");
                                    }
                                }
                                catch
                                {
                                }
                                try
                                {
                                    if (!string.IsNullOrEmpty(txtExtrEx.InnerException.InnerException.Message))
                                    {
                                        sb.Append("----InnerException Message:@");
                                        sb.Append("----" + txtExtrEx.InnerException.InnerException.Message);
                                        sb.Append("@@");
                                    }
                                }
                                catch
                                {
                                }
                                try
                                {
                                    if (
                                        !string.IsNullOrEmpty(
                                            txtExtrEx.InnerException.InnerException.InnerException.Message))
                                    {
                                        sb.Append("------InnerException Message:@");
                                        sb.Append("------"
                                                  + txtExtrEx.InnerException.InnerException.InnerException.Message);
                                    }
                                }
                                catch
                                {

                                }

                                #endregion

                                #region MessageBox

                                ////Add MessageBox with timer, here!
                                //sb.AppendFormat("Press YES to skip the extraction of ID {0} and continue. Press CANCEL to exit.", reader[0]);
                                //DialogResult result = MessageBox.Show(sb.ToString().Replace("@", Environment.NewLine), "Text Caption",
                                //    MessageBoxButtons.YesNo);

                                //if (result == DialogResult.Yes)
                                //{
                                //    continue;
                                //}
                                //else if (result == DialogResult.No)
                                //{
                                //    _shouldClose = true;
                                //    throw new Exception("TextExtractionException", txtExtrEx);
                                //}

                                #endregion

                                _shouldClose = false;
                            }
                        }
                    }
                }

                catch (SqlException sqlException)
                {
                    _shouldClose = true;
                    throw new Exception("SqlException", sqlException);
                }
                catch (Exception exception)
                {
                    _shouldClose = true;
                    throw new Exception("Exception", exception);
                }

                finally
                {
                    //bool should close yes? close
                    if (_shouldClose)
                        sqlCon.Close();
                }
            }
        }

        private static string _reportStartExtracting(IDataRecord reader)
        {
            //todo  to be replaced or/and extended by log4net logs
            return string.Format("START EXTRACTING -- EditionId:{0}, CardId:{1},  CardName:{2}, FileExt:{3}", reader[0],
                reader[1], reader[4], reader[6]);
        }

        private static string _reportErrorExtracting(IDataRecord reader)
        {
            //todo  to be replaced or/and extended by log4net logs
            return string.Format("EXTRACTION FAILED IN -- EditionId:{0}, CardId:{1},  CardName:{2}, FileExt:{3}",
                reader[0], reader[1], reader[4], reader[6]);
        }

        private static CardCriterion _extractRowToCriterion(IDataRecord record)
        {
            CardCriterion criterion = new CardCriterion();
            var textExtractionResult = new TextExtractor().Extract((byte[]) record[5]);

            criterion.EditionId = Convert.ToUInt32(record[0]);
            criterion.CardId = Convert.ToUInt32(record[1]);
            criterion.Territory = Convert.ToString(record[2]);
            criterion.CardKind = Convert.ToString(record[3]);
            criterion.CardName = Convert.ToString(record[4]);
            criterion.CardEdition = textExtractionResult.Text;
            criterion.ContentMetadataDict = textExtractionResult.Metadata;
            criterion.FileExt = Convert.ToString(record[6]);
            criterion.CardAdoptionKindSubject = Convert.ToString(record[7]);
            criterion.CardAdoptionSubject = Convert.ToString(record[8]);
            criterion.CardAdoptionDate = Convert.ToDateTime(record[9]);
            criterion.CardAdoptionNumber = Convert.ToString(record[10]);

            return criterion;
        }

        public static event EventHandler<ProgressBarEventArgs> ProgressEvent;

        public static void Function(int progress) { OnRaiseProgressEvent(new ProgressBarEventArgs(progress)); }

        public static void Function(int progress, int maximum)
        {
            OnRaiseProgressEvent(new ProgressBarEventArgs(progress, maximum));
        }

        private static void OnRaiseProgressEvent(ProgressBarEventArgs e)
        {
            EventHandler<ProgressBarEventArgs> handler = ProgressEvent;
            if (handler != null)
            {
                //this is what actually raises the event.
                handler(null, e);
            }
        }

    }

}