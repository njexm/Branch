using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Branch.com.proem.exm.domain;
using Branch.com.proem.exm.util;
using MySql.Data.MySqlClient;

namespace Branch.com.proem.exm.dao.branch
{
    public class BranchAssociatorInfoDao : MysqlDBHelper
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(BranchAssociatorInfoDao));

        /// <summary>
        /// 添加AssociatorInfo
        /// </summary>
        /// <param name="list"></param>
        public void AddAssociatorInfo(List<AssociatorInfo> list)
        {
            string sql = "insert into zc_associator_info values (@id, @createTime, @updateTime, @AccumulatedCredit, @Address, @AdmissionDate, "
                + "@Age, @Amount, @AmountStartDate, @AmountValidityDate, @BackupAddress, @BirthDay, @CardNumber, @CardId, @Category, @Certificate, "
                + "@CertificateNumber, @ConsumeAmount, @ConsumeFrequency, @Credit, @CreditStartDate, @CreditValidityDate, @DegreeofeDucation, "
                + "@DepositAmount, @Email, @Ethnic, @Issuers, @IssuingDate, @MaritalStatus, @MobilePhone, @Name, @Note, @Password, @RegisterStore, "
                + "@RepeatPassword, @ReviseDate, @Salesman, @Sex, @State, @TelePhone, @UsedCredit, @ZipCode, @DelFlag);";
            MySqlConnection conn = null;
            MySqlTransaction tran = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                tran = conn.BeginTransaction();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                foreach (AssociatorInfo obj in list)
                {
                    cmd.Parameters.AddWithValue("@id" ,obj.Id);
                    cmd.Parameters.AddWithValue("@createTime" ,obj.CreateTime);
                    cmd.Parameters.AddWithValue("@updateTime" ,obj.UpdateTime);
                    cmd.Parameters.AddWithValue("@AccumulatedCredit", obj.AccumulatedCredit);
                    cmd.Parameters.AddWithValue("@Address" , obj.Address);
                    cmd.Parameters.AddWithValue("@AdmissionDate" , obj.AdmissionDate);
                    cmd.Parameters.AddWithValue("@Age" , obj.Age);
                    cmd.Parameters.AddWithValue("@Amount" , obj.Amount);
                    cmd.Parameters.AddWithValue("@AmountStartDate" , obj.AmountStartDate);
                    cmd.Parameters.AddWithValue("@AmountValidityDate" , obj.AmountValidityDate);
                    cmd.Parameters.AddWithValue("@BackupAddress" , obj.BackupAddress);
                    cmd.Parameters.AddWithValue("@BirthDay" , obj.BirthDay);
                    cmd.Parameters.AddWithValue("@CardNumber" , obj.CardNumber);
                    cmd.Parameters.AddWithValue("@CardId" , obj.CardId);
                    cmd.Parameters.AddWithValue("@Category" , obj.Category);
                    cmd.Parameters.AddWithValue("@Certificate" , obj.Certificate);
                    cmd.Parameters.AddWithValue("@CertificateNumber" , obj.CertificateNumber);
                    cmd.Parameters.AddWithValue("@ConsumeAmount" , obj.ConsumeAmount);
                    cmd.Parameters.AddWithValue("@ConsumeFrequency" , obj.ConsumeFrequency);
                    cmd.Parameters.AddWithValue("@Credit" , obj.Credit);
                    cmd.Parameters.AddWithValue("@CreditStartDate" , obj.CreditStartDate);
                    cmd.Parameters.AddWithValue("@CreditValidityDate" , obj.CreditValidityDate);
                    cmd.Parameters.AddWithValue("@DegreeofeDucation" ,obj.DegreeofeDucation);
                    cmd.Parameters.AddWithValue("@DepositAmount" ,obj.DepositAmount);
                    cmd.Parameters.AddWithValue("@Email" , obj.Email);
                    cmd.Parameters.AddWithValue("@Ethnic" , obj.Ethnic);
                    cmd.Parameters.AddWithValue("@Issuers" , obj.Issuers);
                    cmd.Parameters.AddWithValue("@IssuingDate" ,obj.IssuingDate);
                    cmd.Parameters.AddWithValue("@MaritalStatus" , obj.MaritalStatus);
                    cmd.Parameters.AddWithValue("@MobilePhone" , obj.MobilePhone);
                    cmd.Parameters.AddWithValue("@Name" , obj.Name);
                    cmd.Parameters.AddWithValue("@Note" , obj.Note);
                    cmd.Parameters.AddWithValue("@Password" , obj.Password);
                    cmd.Parameters.AddWithValue("@RegisterStore" , obj.RegisterStore);
                    cmd.Parameters.AddWithValue("@RepeatPassword" , obj.RepeatPassword);
                    cmd.Parameters.AddWithValue("@ReviseDate" , obj.ReviseDate);
                    cmd.Parameters.AddWithValue("@Salesman" , obj.Salesman);
                    cmd.Parameters.AddWithValue("@Sex" ,obj.Sex);
                    cmd.Parameters.AddWithValue("@State" , obj.State);
                    cmd.Parameters.AddWithValue("@TelePhone" ,obj.TelePhone);
                    cmd.Parameters.AddWithValue("@UsedCredit" ,obj.UsedCredit);
                    cmd.Parameters.AddWithValue("@ZipCode" , obj.ZipCode);
                    cmd.Parameters.AddWithValue("@DelFlag" , obj.DelFlag);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                log.Error("添加数据到zc_associator_info发生异常", ex);
            }
            finally
            { 
                cmd.Dispose();
                tran.Dispose();
                CloseConnection(conn);
            }
        }


        public AssociatorInfo FindById(string p)
        {
            AssociatorInfo obj = new AssociatorInfo();
            string sql = "select id, CREATETIME, UPDATETIME, ASSOCIATOR_ACCUMULATEDCREDIT, ASSOCIATOR_ADDRESS, ASSOCIATOR_ADMISSIONDATE, "
                + "ASSOCIATOR_AGE, ASSOCIATOR_AMOUNT, ASSOCIATOR_AMOUNTSTARTDATE, ASSOCIATOR_AMOUNTVALIDITYDATE, ASSOCIATOR_BACKUPADDRESS, "
                + " ASSOCIATOR_BIRTHDAY, ASSOCIATOR_CARDNUMBER, ASSOCIATOR_CARDID, ASSOCIATOR_CATEGORY, ASSOCIATOR_CERTIFICATE, "
                + " ASSOCIATOR_CERTIFICATENUMBER, ASSOCIATOR_CONSUMEAMOUNT, ASSOCIATOR_CONSUMEFREQUENCY, ASSOCIATOR_CREDIT, "
                + " ASSOCIATOR_CREDITSTARTDATE, ASSOCIATOR_CREDITVALIDITYDATE, ASSOCIATOR_DEGREEOFEDUCATION, ASSOCIATOR_DEPOSITAMOUNT, "
                + " ASSOCIATOR_EMAIL, ASSOCIATOR_ETHNIC, ASSOCIATOR_ISSUERS, ASSOCIATOR_ISSUINGDATE, ASSOCIATOR_MARITALSTATUS, "
                + " ASSOCIATOR_MOBILEPHONE,ASSOCIATOR_NAME, ASSOCIATOR_NOTE, ASSOCIATOR_PASSWORD, ASSOCIATOR_REGISTERSTORE, "
                + " ASSOCIATOR_REPEATPASSWORD, ASSOCIATOR_REVISEDATE, ASSOCIATOR_SALESMAN, ASSOCIATOR_SEX, ASSOCIATOR_STATE, ASSOCIATOR_TELEPHONE, "
                + " ASSOCIATOR_USEDCREDIT, ASSOCIATOR_ZIPCODE, DELFLAG from zc_associator_info where id = '"+p+"'";
            MySqlConnection conn = null;
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                conn = GetConnection();
                cmd.Connection = conn;
                cmd.CommandText = sql;
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    obj.Id = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    obj.CreateTime = reader.IsDBNull(1) ? default(DateTime) : reader.GetDateTime(1);
                    obj.UpdateTime = reader.IsDBNull(2) ? default(DateTime) : reader.GetDateTime(2);
                    obj.AccumulatedCredit = reader.IsDBNull(3) ? default(int) : reader.GetInt32(3);
                    obj.Address = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    obj.AdmissionDate = reader.IsDBNull(5) ? default(DateTime) : reader.GetDateTime(5);
                    obj.Age = reader.IsDBNull(6) ? default(int) : reader.GetInt32(6);
                    obj.Amount = reader.IsDBNull(7) ? default(float) : reader.GetFloat(7);
                    obj.AmountStartDate = reader.IsDBNull(8) ? default(DateTime) : reader.GetDateTime(8);
                    obj.AmountValidityDate = reader.IsDBNull(9) ? default(DateTime) : reader.GetDateTime(9);
                    obj.BackupAddress = reader.IsDBNull(10) ? string.Empty : reader.GetString(10);
                    obj.BirthDay = reader.IsDBNull(11) ? default(DateTime) : reader.GetDateTime(11);
                    obj.CardNumber = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                    obj.CardId = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                    obj.Category = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                    obj.Certificate = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                    obj.CertificateNumber = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                    obj.ConsumeAmount = reader.IsDBNull(17) ? default(float) : reader.GetFloat(17);
                    obj.ConsumeFrequency = reader.IsDBNull(18) ? default(int) : reader.GetInt32(18);
                    obj.Credit = reader.IsDBNull(19) ? default(int) : reader.GetInt32(19);
                    obj.CreditStartDate = reader.IsDBNull(20) ? default(DateTime) : reader.GetDateTime(20);
                    obj.CreditValidityDate = reader.IsDBNull(21) ? default(DateTime) : reader.GetDateTime(21);
                    obj.DegreeofeDucation = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                    obj.DepositAmount = reader.IsDBNull(23) ? default(float) : reader.GetFloat(23);
                    obj.Email = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                    obj.Ethnic = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                    obj.Issuers = reader.IsDBNull(26) ? string.Empty : reader.GetString(26);
                    obj.IssuingDate = reader.IsDBNull(27) ? default(DateTime) : reader.GetDateTime(27);
                    obj.MaritalStatus = reader.IsDBNull(28) ? string.Empty : reader.GetString(28);
                    obj.MobilePhone = reader.IsDBNull(29) ? string.Empty : reader.GetString(29);
                    obj.Name = reader.IsDBNull(30) ? string.Empty : reader.GetString(30);
                    obj.Note = reader.IsDBNull(31) ? string.Empty : reader.GetString(31);
                    obj.Password = reader.IsDBNull(32) ? string.Empty : reader.GetString(32);
                    obj.RegisterStore = reader.IsDBNull(33) ? string.Empty : reader.GetString(33);
                    obj.RepeatPassword = reader.IsDBNull(34) ? string.Empty : reader.GetString(34);
                    obj.ReviseDate = reader.IsDBNull(35) ? default(DateTime) : reader.GetDateTime(35);
                    obj.Salesman = reader.IsDBNull(36) ? string.Empty : reader.GetString(36);
                    obj.Sex = reader.IsDBNull(37) ? string.Empty : reader.GetString(37);
                    obj.State = reader.IsDBNull(38) ? string.Empty : reader.GetString(38);
                    obj.TelePhone = reader.IsDBNull(39) ? string.Empty : reader.GetString(39);
                    obj.UsedCredit = reader.IsDBNull(40) ? default(int) : reader.GetInt32(40);
                    obj.ZipCode = reader.IsDBNull(41) ? string.Empty : reader.GetString(41);
                    obj.DelFlag = reader.IsDBNull(42) ? string.Empty : reader.GetString(42);
                }
            }
            catch (Exception ex)
            {
                log.Error("根据Id查询会员信息发生错误", ex);
            }
            finally
            {
                CloseConnection(conn);
            }
            return obj;
        }
    }
}
