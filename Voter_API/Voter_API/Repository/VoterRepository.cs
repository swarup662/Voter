using System.Data;
using Voter_API.Interfaces;
using Voter_API.Models;
using Voter_API.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace Voter_API.Repository
{
    public class VoterRepository : IVOTERRepository
    {





        private readonly IConfiguration _configuration;
        private readonly DbAccess _DbAccess;
        private readonly ILogger<VoterRepository> _logger;

        public VoterRepository(IConfiguration configuration, ILogger<VoterRepository> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _DbAccess = new DbAccess(_configuration);
        }




        //public List<VoterAPIModel> GetVote(int VOTER_KEY)
        //{
        //    try
        //    {
        //        string[] ParametersNames = { "@VOTER_KEY" };
        //        string[] ParametersValues = { VOTER_KEY.ToString() };

        //        DataSet dataSet = _DbAccess.Ds_Process("SP_GET_VOTER_DB", ParametersNames, ParametersValues);
        //        if (dataSet == null)
        //        {
        //            _logger.LogError("DataSet returned null from SP_GET_VOTER_DB.");
        //            return new List<VoterAPIModel>();
        //        }

        //        List<VoterAPIModel> lst = new List<VoterAPIModel>();
        //        if (dataSet.Tables.Count > 0)
        //        {
        //            DataTable dt = dataSet.Tables[0];
        //            if (dt == null)
        //            {
        //                _logger.LogError("DataTable is null.");
        //                return new List<VoterAPIModel>();
        //            }

        //            if (dt.Rows.Count > 0)
        //            {
        //                foreach (DataRow row in dt.Rows)
        //                {
        //                        var obj = new VoterAPIModel();
        //                        obj.VOTER_KEY = Convert.ToInt32(row["VOTER_KEY"]);
        //                        obj.NAME = Convert.ToString(row["NAME"]);
        //                        obj.AGE = Convert.ToInt32(row["AGE"]);
        //                        obj.GENDER_ID = Convert.ToInt32(row["GENDER_ID"]);
        //                        obj.GENDER_NAME = Convert.ToString(row["GENDER_NAME"]);
        //                        obj.STATE_ID = Convert.ToInt32(row["STATE_ID"]);
        //                        obj.STATE_NAME = Convert.ToString(row["STATE_NAME"]);
        //                        obj.VOTERCARD_NO = Convert.ToString(row["VOTERCARD_NO"]);
        //                        lst.Add(obj);
        //                }
        //            }
        //        }
        //        return lst;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while getting votes.");
        //        throw new Exception(ex.Message);
        //    }
        //}


        public List<VoterAPIModel> GetVote(int VOTER_KEY)
        {
            try
            {
                string[] ParametersNames = { "@VOTER_KEY" };
                string[] ParametersValues = { VOTER_KEY.ToString() };

                DataSet dataSet = _DbAccess.Ds_Process("SP_GET_VOTER_DB", ParametersNames, ParametersValues);
                List<VoterAPIModel> lst = new List<VoterAPIModel>();
                if (dataSet.Tables.Count > 0)
                {

                    DataTable dt = dataSet.Tables[0];

                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            var obj = new VoterAPIModel();
                            obj.VOTER_KEY = Convert.ToInt32(row["VOTER_KEY"]);
                            obj.NAME = Convert.ToString(row["NAME"]);
                            obj.AGE = Convert.ToInt32(row["AGE"]);
                            obj.GENDER_ID = Convert.ToInt32(row["GENDER_ID"]);
                            obj.GENDER_NAME = Convert.ToString(row["GENDER_NAME"]);
                            obj.STATE_ID = Convert.ToInt32(row["STATE_ID"]);
                            obj.STATE_NAME = Convert.ToString(row["STATE_NAME"]);
                            obj.VOTERCARD_NO = Convert.ToString(row["VOTERCARD_NO"]);
                            lst.Add(obj);

                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public int SaveVote(VoterAPIModel model, string REC_TYPE)
        {
            try
            {
                string[] vname = { "@REC_TYPE", "@VOTER_KEY", "@NAME", "@AGE", "@GENDER_ID", "@STATE_ID", "VOTERCARD_NO" };
                string[] vvalue = { REC_TYPE, model.VOTER_KEY.ToString(), model.NAME, model.AGE.ToString(), model.GENDER_ID.ToString(), model.STATE_ID.ToString(), model.VOTERCARD_NO };
                int result = _DbAccess.int_Process("SP_CRUD_VOTER_DB", vname, vvalue);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving vote.");
                throw;
            }
        }

        public int DeleteVote(VoterAPIModel model, string REC_TYPE)
        {
            try
            {
                string[] vname = { "@REC_TYPE", "@VOTER_KEY", "@NAME", "@AGE", "@GENDER_ID", "@STATE_ID", "VOTERCARD_NO" };
                string[] vvalue = { REC_TYPE, model.VOTER_KEY.ToString(), "", "", "", "", "" };
                int result = _DbAccess.int_Process("SP_CRUD_VOTER_DB", vname, vvalue);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting vote.");
                throw;
            }
        }



        public List<VoterAPIModel> getState(int STATE_ID)
        {
            try
            {
                string[] ParametersNames = { "@STATE_ID" };
                string[] ParametersValues = { STATE_ID.ToString() };

                DataSet dataSet = _DbAccess.Ds_Process("SP_GET_STATE", ParametersNames, ParametersValues);
                if (dataSet == null)
                {
                    _logger.LogError("DataSet returned null from SP_GET_STATE.");
                    return new List<VoterAPIModel>();
                }

                List<VoterAPIModel> lst = new List<VoterAPIModel>();
                if (dataSet.Tables.Count > 0)
                {
                    DataTable dt = dataSet.Tables[0];
                    if (dt == null)
                    {
                        _logger.LogError("DataTable is null.");
                        return new List<VoterAPIModel>();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            var obj = new VoterAPIModel
                            {
                                STATE_ID = Convert.ToInt32(row["STATE_ID"]),
                                STATE_NAME = Convert.ToString(row["STATE_NAME"])
                            };
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting state.");
                throw new Exception(ex.Message);
            }
        }




        public List<VoterAPIModel> getGender(int GENDER_ID)
        {
            try
            {
                string[] ParametersNames = { "@GENDER_ID" };
                string[] ParametersValues = { GENDER_ID.ToString() };

                DataSet dataSet = _DbAccess.Ds_Process("SP_GET_GENDER", ParametersNames, ParametersValues);
                if (dataSet == null)
                {
                    _logger.LogError("DataSet returned null from SP_GET_GENDER.");
                    return new List<VoterAPIModel>();
                }

                List<VoterAPIModel> lst = new List<VoterAPIModel>();
                if (dataSet.Tables.Count > 0)
                {
                    DataTable dt = dataSet.Tables[0];
                    if (dt == null)
                    {
                        _logger.LogError("DataTable is null.");
                        return new List<VoterAPIModel>();
                    }

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            var obj = new VoterAPIModel
                            {
                                GENDER_ID = Convert.ToInt32(row["GENDER_ID"]),
                                GENDER_NAME = Convert.ToString(row["GENDER_NAME"])
                            };
                            lst.Add(obj);
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting gender.");
                throw new Exception(ex.Message);
            }
        }
    }
}
