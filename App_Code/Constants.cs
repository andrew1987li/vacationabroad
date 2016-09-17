
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Constants
	{
		/// <summary>
		/// Modify these values if you want to use your own profile.
		/// </summary>

		/* 
		 *                                                                         *
		 * WARNING: Do not embed plaintext credentials in your application code.   *
		 * Doing so is insecure and against best practices.                        *
		 *                                                                         *
		 * Your API credentials must be handled securely. Please consider          *
		 * encrypting them for use in any production environment, and ensure       *
		 * that only authorized individuals may view or modify them.               *
		 *                                                                         *
		 */

		public const string API_USERNAME = "ar_api1.vacations-abroad.com";
		public const string API_PASSWORD = "LRGWHWGHNW27LYUB";
		public const string API_SIGNATURE = "AfmqlSNFon3OD9WfPiXma-yIMskQAQ54Ekk5PiLkj52c0CDp0fjepyA6";
//		public const string CERTIFICATE = "sdk-seller.p12";
//		public const string PRIVATE_KEY_PASSWORD = "password";
		

//		public const string API_USERNAME = "sdk-three_api1.sdk.com";
//		public const string API_PASSWORD = "QFZCWN5HZM8VBG7Q";
//		public const string API_SIGNATURE = "A21eW1ch..NEqJJ-glaLhqkBMlzeAsWqX0ycck-CTc0tKI4pa1u.rgNF";		
		
		
		public const string CERTIFICATE = "";
		public const string PRIVATE_KEY_PASSWORD = "";
		public const string SUBJECT = "";
		//public const string ENVIRONMENT = "sandbox";
        public const string ENVIRONMENT = "live";
		public const string ECURLLOGIN = "https://developer.paypal.com";
		//public const string ENVIRONMENT = "beta-sandbox";
		//public const string ECURLLOGIN = "https://beta-developer.paypal.com";


		public const string PROFILE_KEY = "Profile";
		public const string PAYMENT_ACTION_PARAM = "paymentAction";
		public const string PAYMENT_TYPE_PARAM = "paymentType";


		public const string STANDARD_EMAIL_ADDRESS = "sdk-seller@sdk.com";

		//public const string PAYPAL_URL = "https://www.sandbox.paypal.com";
        public const string PAYPAL_URL = "https://www.paypal.com/cgi-bin/webscr"; 
		public const string WEBSCR_URL = PAYPAL_URL + "/cgi-bin/webscr";

				
	}
