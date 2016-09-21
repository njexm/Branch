using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Config
/// </summary>
namespace Branch.app_code
{
    public class Config
    {

        public static string alipay_public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDDI6d306Q8fIfCOaTXyiUeJHkrIvYISRcc73s3vF1ZT7XN8RNPwJxo8pWaJMmvyTn9N4HQ632qJBVHf8sxHi/fEsraprwCtzvzQETrNRwVxLO5jVmRGi60j8Ue1efIlzPXV9je9mkjzOmdssymZkh2QhUrCmZYI/FCEa3/cNMW0QIDAQAB";
        //这里要配置没有经过的原始私钥
        public static string merchant_private_key = @"MIICWwIBAAKBgQC/VVmFd/HmkCShA1wh7JDdhKwDCRc9xLHlbf8/6H1ludiPVQDdHiEauyPAH1DtPBsH6lSZDleHd6zxqkRCDBW9hm7ezvZ7G8SZ8b93r6sLrSMigfAPSKsFGwg+swF3PdhtIS76wMYdFZHtniIQutouWpXTFue1SFFlgvI5wsMcVwIDAQABAoGAKmt/14mKRjX7CP+3s4XBcHtzr5CaaIVtc5J5nu+qrZ0QmGaeNGfzqvuGp5fJ128WByE69Fwrp8suur2nOIV5ZBNC7Xz1Z8olw31l7DPsvkSSY/v2BL+CxN3Yo9J4Vzq3gZ1TFegapycmtXHCibGzKXJsMVPOIPl9cksX/ASS+bECQQDtcTLYKlIZLXFhUw+63xhxyKjhLr4p/EeO2i+KC13WOztYLM5B8bgAZYqWBpxPBgeMVLTZzEJHuWuNsWu7JLizAkEAzkmYX8EMForv/NL9QoyN+LUzo+Qnejzqtgz6I5EvO7r/39R8AV/gsvgVgERpCQsIA/ZrTZKbx1xSvzGrGYB3zQJABxGKoPLJlORMbshJPJfto12E4YdBtABFpRQLvEFQXtNOlfZngYEPqDtdn8+8kYGtcdkHtUAmbPoxIHu6qEyUjwJAH7JjPWNWTihvE7P/wjgCoJKHKsml3zx4/BwmW0y1q0HiiywFGdj3l8hS1XCtivws8R7uAe9jDFD9Te990N9SiQJAI2Sh0o3MJrnf2VxchJm00ZtSDeXKZroKnwphQPM0UMhcCNyEmcrsz5Wh39bPw3bMaM9A2wpImzwC3C43m/Y0Mg==";
        public static string merchant_public_key = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCnxj/9qwVfgoUh/y2W89L6BkRAFljhNhgPdyPuBV64bfQNN1PjbCzkIM6qRdKBoLPXmKKMiFYnkd6rAoprih3/PrQEB/VsW8OoM8fxn67UDYuyBTqA23MML9q1+ilIZwBC2AQ2UBVOrFXfFl75p6/B5KsiNG9zpgmLCUYuLkxpLQIDAQAB";
        //此处填写开发者应用appid
        public static string appId = "2016072701672199";
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";
        public static string pid = "2088221665554288";


        public static string charset = "utf-8";//"utf-8";
        public static string sign_type = "RSA";
        public static string version = "1.0";


        public Config()
        {
            //
        }

        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

    }
}