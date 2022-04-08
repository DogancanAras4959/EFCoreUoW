using System;
using System.IO;
using System.Text;
using System.Net;
using System.Xml;

namespace ePayment
{
    public class cc5payment
    {
        private string _Host = "";
        private string _Name = "";
        private string _Password = "";
        private string _ClientId = "";
        private string _IP = "";
        private string _CardNumber = "";
        private string _ExpMonth = "";
        private string _ExpYear = "";
        private string _Cv2 = "";
        private string _Currency = "";
        private string _ChargeType = "";
        private string _GroupId = "";
        private string _Total = "";
        private string _TaxTotal = "";
        private string _ShipTotal = "";
        private string _Ordno = "";
        private string _Err = "";
        private string _Time = "";
        private string _Result = "";
        private string _Taksit = "";
        private string _Comments = "";
        private string _SState = "";
        private string _BState = "";
        private string _BCompany = "";
        private string _ReferNum = "";
        private string _Fax = "";
        private string _Port = "";
        private string _UserId = "";
        private int _OrderResult = 0;
        private string _SCountry = "";
        private string _SZip = "";
        private string _SCity = "";
        private string _SAddr3 = "";
        private string _SAddr2 = "";
        private string _SAddr1 = "";
        private string _SName = "";
        private string _BCountry = "";
        private string _BZip = "";
        private string _BCity = "";
        private string _BAddr3 = "";
        private string _BAddr2 = "";
        private string _BAddr1 = "";
        private string _BName = "";
        private string _Phone = "";
        private string _Email = "";
        private string _SubTotal = "";
        private string _CardholderPresentCode = "";
        private string _PayerSecurityLevel = "";
        private string _PayerAuthenticationCode = "";
        private string _PayerTxnId = "";

        public string _OrderType = "";
        public string _TotalNumberPayments = "";
        public string _OrderFrequencyCycle = "";
        public string _OrderFrequencyInterval = "";

        //return values
        private string _ErrMsg = "";
        private string _Appr = "";
        private string _Oid = "";
        private string _Code = "";
        private string _RefNo = "";
        private string _ProcReturnCode = "";
        private string _TransId = "";
        //Extra
        public XmlDocument ExtraDoc;
        public string _FNameExtra = "";
        public string _FValueExtra = "";

        private string dummy = "";

        private static bool configurated;
        private static object configurationLockObject;

        static cc5payment()
        {
            configurated = false;
            configurationLockObject = new object();
        }

        //internal
        private XmlDocument ReqDoc;

        private XmlNode ReqRoot;
        public cc5payment()
        {
            ReqDoc = new XmlDocument();
            ReqDoc.LoadXml("<?xml version=\"1.0\" encoding=\"ISO-8859-9\"?><CC5Request><OrderItemList></OrderItemList><Extra></Extra></CC5Request>");
            ReqRoot = ReqDoc.DocumentElement;
            //XmlNode elem = ReqDoc.CreateNode(XmlNodeType.Element,"OrderItemList","");
            //ReqRoot.AppendChild(elem);
            //XmlNode elemEx = ReqDoc.CreateNode(XmlNodeType.Element,"Extra","");
            //ReqRoot.AppendChild(elemEx);
            dummy = ReqRoot.OuterXml;
            ExtraDoc = new XmlDocument();
        }

        private void AddNode(String name, String val)
        {
            XmlElement elem = ReqDoc.CreateElement(name);
            string val2 = charExpandAll(val);
            elem.InnerText = val2;
            ReqRoot.AppendChild(elem);
        }

        private void AddNodeReq(XmlDocument req2, String name, String val)
        {
            XmlElement elem = req2.CreateElement(name);
            string val2 = charExpandAll(val);
            elem.InnerText = val2;
            req2.DocumentElement.AppendChild(elem);
        }
        private void AddToNode(XmlDocument req2, XmlNode NodeReq2, String name, String val)
        {
            XmlElement elem = req2.CreateElement(name);
            string val2 = charExpandAll(val);
            elem.InnerText = val2;
            NodeReq2.AppendChild(elem);
        }
        private string charExpandAll(string val)
        {
            //***************************epayapi den comment olmayan char expand lar aynen gecilmeli
            /*
            charExpand(in,'%',"%25");//Percent character ("%") Hex:25 Dec:37 	bu escape icin kullaniliyor o yuzden en basa aldim
            //charExpand(in,'&',"%26");//Ampersand ("&")
            charExpand(in,'&',"%BF");//Ampersand ("&") Inverted Question Mark  yapiliyor XML i bozuyor
            charExpand(in,';',"%3B");//Semi-colon (";") bu iki karakter <> larin encode edilmesinde kullaniliyor
            charExpand(in,'<',"%BF");//Less Than symbol ("<")Hex:3C Dec:60 Inverted Question Mark  yapiliyor XML i bozuyor tag baslangici gibi algilaniyor
            //charExpand(in,'<',"%3C");//Less Than symbol ("<")Hex:3C Dec:60
            //charExpand(in,'>',"%3E");//Greater Than symbol (">") Hex:3E Dec:62
            //charExpand(in, '<',"&lt;");
            charExpand(in, '>',"&gt;");
            charExpand(in,'$',"%24");//Dollar ("$")
            charExpand(in,'+',"%2B");//Plus ("+")
            charExpand(in,',',"%2C");//Comma (",")
            charExpand(in,'/',"%2F");//Forward slash/Virgule ("/")
            charExpand(in,':',"%3A");//Colon (":")

            charExpand(in,'=',"%3D");//Equals ("=")
            charExpand(in,'?',"%3F");//Question mark ("?")
            charExpand(in,'@',"%40");//'At' symbol ("@")

            //charExpand(in,' ',"%20");//Space Hex:20 Dec:32
            charExpand(in,'#',"%23");//Pound character ("#") Hex:23 Dec:35 
            //charExpand(in,'%',"%25");//Percent character ("%") Hex:25 Dec:37  en basa alinmali

            charExpand(in,'{',"%7B");//Left Curly Brace ("{") Hex:7B Dec:12
            charExpand(in,'}',"%7D");//Right Curly Brace ("}") Hex:7D Dec:125
            charExpand(in,'|',"%7C");//Vertical Bar/Pipe ("|") Hex:7C Dec:124
            charExpand(in,'\\',"%5C");//Backslash ("\") Hex:5C Dec:92
            charExpand(in,'^',"%5E");//Caret ("^") Hex:5E Dec:94
            charExpand(in,'~',"%7E");//Tilde ("~") Hex:7E Dec:126
            charExpand(in,'[',"%5B");//Left Square Bracket ("[") Hex:5B Dec:91
            charExpand(in,']',"%5D");//Right Square Bracket ("]") Hex:5D Dec:93
            charExpand(in,'`',"%60");//Grave Accent ("`") Hex:60  Dec:96 

            charExpand(in, (char) 351, "%FE");    //	ş
            charExpand(in, (char) 287, "%F0");    //	ğ
            charExpand(in, (char) 350, "%DE");    //	Ş
            charExpand(in, (char) 286, "%D0");    //	Ğ
            //charExpand(in, '\r', " ");
            //charExpand(in, '\n', " ");  // 0X0D  ve 0X0A    carriage return & line feed
            charExpand(in, (char)10, " ");
            charExpand(in, (char)13, " "); 
            charExpand(in ,(char)	179	,"%B3");    //	Power of 3	
            charExpand(in ,(char)	191	,"%BF");    //	Inverted Question Mark	
            charExpand(in ,(char)	192	,"%C0");    //	A Grave	
            charExpand(in ,(char)	195	,"%C3");    //	A Tilde	
            charExpand(in ,(char)	196	,"%C4");    //	A Diaeresis	
            charExpand(in ,(char)	215	,"%3F");    //	Multiply (x)	
            charExpand(in ,(char)	217	,"%D9");    //	U Grave	
            charExpand(in ,(char)	218	,"%DA");    //	U Acute	
            charExpand(in ,(char)	222	,"%DE");    //	S Cedilla	
            */
            //**************************************
            string val2 = charExpand(val, (char)37, "%25");    //	Percent	%
            val2 = charExpand(val2, '&', "%BF");//Ampersand ("&") Inverted Question Mark  yapiliyor XML i bozuyor
            val2 = charExpand(val2, ';', "%3B");//Semi-colon (";") bu iki karakter <> larin encode edilmesinde kullaniliyor
            val2 = charExpand(val2, '<', "%BF");//Less Than symbol ("<")Hex:3C Dec:60 Inverted Question Mark  yapiliyor XML i bozuyor tag baslangici gibi algilaniyor

            //val2 = charExpand(val2, '>', "&gt;");
            val2 = charExpand(val2, '>', "%3E");//Greater Than symbol (">") Hex:3E Dec:62

            val2 = charExpand(val2, '$', "%24");//Dollar ("$")
            val2 = charExpand(val2, '+', "%2B");//Plus ("+")
            val2 = charExpand(val2, ',', "%2C");//Comma (",")
            val2 = charExpand(val2, '/', "%2F");//Forward slash/Virgule ("/")
            val2 = charExpand(val2, ':', "%3A");//Colon (":")

            val2 = charExpand(val2, '=', "%3D");//Equals ("=")
            val2 = charExpand(val2, '?', "%3F");//Question mark ("?")
            val2 = charExpand(val2, '@', "%40");//'At' symbol ("@")

            val2 = charExpand(val2, '#', "%23");//Pound character ("#") Hex:23 Dec:35

            val2 = charExpand(val2, '{', "%7B");//Left Curly Brace ("{") Hex:7B Dec:12
            val2 = charExpand(val2, '}', "%7D");//Right Curly Brace ("}") Hex:7D Dec:125
            val2 = charExpand(val2, '|', "%7C");//Vertical Bar/Pipe ("|") Hex:7C Dec:124
            val2 = charExpand(val2, '\\', "%5C");//Backslash ("\") Hex:5C Dec:92
            val2 = charExpand(val2, '^', "%5E");//Caret ("^") Hex:5E Dec:94
            val2 = charExpand(val2, '~', "%7E");//Tilde ("~") Hex:7E Dec:126
            val2 = charExpand(val2, '[', "%5B");//Left Square Bracket ("[") Hex:5B Dec:91
            val2 = charExpand(val2, ']', "%5D");//Right Square Bracket ("]") Hex:5D Dec:93
            val2 = charExpand(val2, '`', "%60");//Grave Accent ("`") Hex:60  Dec:96 

            val2 = charExpand(val2, (char)10, " ");
            val2 = charExpand(val2, (char)13, " ");
            val2 = charExpand(val2, (char)179, "%B3");    //	Power of 3	
            val2 = charExpand(val2, (char)191, "%BF");    //	Inverted Question Mark	
            val2 = charExpand(val2, (char)192, "%C0");    //	A Grave	
            val2 = charExpand(val2, (char)195, "%C3");    //	A Tilde	
            val2 = charExpand(val2, (char)196, "%C4");    //	A Diaeresis	
            val2 = charExpand(val2, (char)215, "%3F");    //	Multiply (x)	
            val2 = charExpand(val2, (char)217, "%D9");    //	U Grave	
            val2 = charExpand(val2, (char)218, "%DA");    //	U Acute	
            val2 = charExpand(val2, (char)222, "%DE");    //	S Cedilla	

            /*
			 * 
			val2 = charExpand(val ,(char)32 , "%20"); //space  X
			val2 = charExpand(val2 ,(char)	33	,"%21");    //	Exclamation Point	! 
			val2 = charExpand(val2 ,(char)	34	,"%22");    //	Double Quote	"
			//val2 = charExpand(val2 ,(char)	35	,"%23");    //	Number/Pound	#
			//val2 = charExpand(val2 ,(char)	36	,"%24");    //	Dollars	$
			
			//val2 = charExpand(val2 ,(char)	38	,"%26");    //	Ampersand	&
			//val2 = charExpand(val2 ,(char)	39	,"%27");    //	Single Quote	'
			//val2 = charExpand(val2 ,(char)	40	,"%28");    //	Left Parenthesis	(
			//val2 = charExpand(val2 ,(char)	41	,"%29");    //	Right Parenthesis	)
			//val2 = charExpand(val2 ,(char)	42	,"%2a");    //	Asterisk	*
			//val2 = charExpand(val2 ,(char)	43	,"%2B");    //	Plus	+
			//val2 = charExpand(val2 ,(char)	44	,"%2C");    //	Comma	,
			//val2 = charExpand(val2 ,(char)	45	,"%2D");    //	Hyphen	-
			val2 = charExpand(val2 ,(char)	46	,"%2E");    //	Period	.
			//val2 = charExpand(val2 ,(char)	47	,"%2f");    //	Forward Slash	/
			//val2 = charExpand(val2 ,(char)	58	,"%3a");    //	Colon	:
			//val2 = charExpand(val2 ,(char)	59	,"%3b");    //	Semicolon	;
			//val2 = charExpand(val2 ,(char)	60	,"%3C");    //	Less Than	<
			//val2 = charExpand(val2 ,(char)	60	,"&lt;");    //	Less Than	<
			//val2 = charExpand(val2 ,(char)	61	,"%3D");    //	Equals	=
			// <> isaretlerini .net kendisi &lt;  ve &gt;   isaretlerine ceviriyor
			//val2 = charExpand(val2 ,(char)	62	,"%3E");    //	Greater Than	>
			//val2 = charExpand(val2 ,(char)	62	,"&gt;");    //	Greater Than	>
			//val2 = charExpand(val2 ,(char)	63	,"%3f");    //	Question Mark	?
			//val2 = charExpand(val2 ,(char)	64	,"%40");    //	Commercial At	@
			//val2 = charExpand(val2 ,(char)	91	,"%5b");    //	Left Bracket	[
			//val2 = charExpand(val2 ,(char)	92	,"%5c");    //	Backslash	\
			//val2 = charExpand(val2 ,(char)	93	,"%5d");    //	Right Bracket	]
			//val2 = charExpand(val2 ,(char)	94	,"%5e");    //	Caret (Circonflex)	^
			//val2 = charExpand(val2 ,(char)	95	,"%5f");    //	Underscore	_
			val2 = charExpand(val2 ,(char)	96	,"%60");    //	ASCII Grave	`
			val2 = charExpand(val2 ,(char)	123	,"%7B");    //	Left Curly Bracket	{
			val2 = charExpand(val2 ,(char)	124	,"%7C");    //	Broken Vertical Bar	|
			val2 = charExpand(val2 ,(char)	125	,"%7D");    //	Right Curly Bracket	}
			val2 = charExpand(val2 ,(char)	126	,"%7E");    //	Tilde	~
			val2 = charExpand(val2 ,(char)	128	,"%80");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	129	,"%81");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	130	,"%82");    //	(unassigned)	‚
			val2 = charExpand(val2 ,(char)	131	,"%83");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	132	,"%84");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	133	,"%85");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	134	,"%86");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	135	,"%87");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	136	,"%88");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	137	,"%89");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	138	,"%8A");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	139	,"%8B");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	140	,"%8C");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	141	,"%8D");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	142	,"%8E");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	143	,"%8F");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	144	,"%90");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	145	,"%91");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	146	,"%92");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	147	,"%93");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	148	,"%94");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	149	,"%95");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	150	,"%96");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	151	,"%97");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	152	,"%98");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	153	,"%99");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	154	,"%9A");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	155	,"%9B");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	156	,"%9C");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	157	,"%9D");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	158	,"%9E");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	159	,"%9F");    //	(unassigned)	
			val2 = charExpand(val2 ,(char)	160	,"%A0");    //	Hard space	
			val2 = charExpand(val2 ,(char)	161	,"%A1");    //	Inverted Exclamation Point	
			val2 = charExpand(val2 ,(char)	162	,"%A2");    //	Cent	
			val2 = charExpand(val2 ,(char)	163	,"%A3");    //	Pound/Sterling	
			val2 = charExpand(val2 ,(char)	164	,"%A4");    //	General Currency Symbol	
			val2 = charExpand(val2 ,(char)	165	,"%A5");    //	Yen	
			val2 = charExpand(val2 ,(char)	166	,"%A6");    //	Broken Vertical Bar	
			val2 = charExpand(val2 ,(char)	167	,"%A7");    //	Section Sign	
			val2 = charExpand(val2 ,(char)	168	,"%A8");    //	Diaeresis	
			val2 = charExpand(val2 ,(char)	169	,"%A9");    //	Copyright	
			val2 = charExpand(val2 ,(char)	170	,"%AA");    //	Feminine Spanish Ordinal	
			val2 = charExpand(val2 ,(char)	171	,"%Ab");    //	Left Double Guillemet	
			val2 = charExpand(val2 ,(char)	172	,"%AC");    //	Logical Not	
			val2 = charExpand(val2 ,(char)	173	,"%AD");    //	Hyphen	
			val2 = charExpand(val2 ,(char)	174	,"%AE");    //	Registered Trademark	
			val2 = charExpand(val2 ,(char)	175	,"%AF");    //	Overline (Long Mark)	
			val2 = charExpand(val2 ,(char)	176	,"%B0");    //	Small Circle	
			val2 = charExpand(val2 ,(char)	177	,"%B1");    //	Plus or Minus	
			val2 = charExpand(val2 ,(char)	178	,"%B2");    //	Power of 2	
			val2 = charExpand(val2 ,(char)	179	,"%B3");    //	Power of 3	
			val2 = charExpand(val2 ,(char)	180	,"%B4");    //	Acute	
			val2 = charExpand(val2 ,(char)	181	,"%B5");    //	Micro	
			val2 = charExpand(val2 ,(char)	182	,"%B6");    //	Paragraph Sign	
			val2 = charExpand(val2 ,(char)	183	,"%B7");    //	Centered Dot	
			val2 = charExpand(val2 ,(char)	184	,"%B8");    //	Cedilla	
			val2 = charExpand(val2 ,(char)	185	,"%B9");    //	Power of 1	
			val2 = charExpand(val2 ,(char)	186	,"%BA");    //	Masculine Spanish Ordinal	
			val2 = charExpand(val2 ,(char)	187	,"%BB");    //	Right Double Guillemet	
			val2 = charExpand(val2 ,(char)	188	,"%BC");    //	4-Jan	
			val2 = charExpand(val2 ,(char)	189	,"%BD");    //	2-Jan	
			val2 = charExpand(val2 ,(char)	190	,"%BE");    //	4-Mar	
			val2 = charExpand(val2 ,(char)	191	,"%BF");    //	Inverted Question Mark	
			val2 = charExpand(val2 ,(char)	192	,"%C0");    //	A Grave	
			val2 = charExpand(val2 ,(char)	193	,"%C1");    //	A Acute	
			val2 = charExpand(val2 ,(char)	194	,"%C2");    //	A Circumflex	
			val2 = charExpand(val2 ,(char)	195	,"%C3");    //	A Tilde	
			val2 = charExpand(val2 ,(char)	196	,"%C4");    //	A Diaeresis	
			val2 = charExpand(val2 ,(char)	197	,"%C5");    //	A Ring	
			val2 = charExpand(val2 ,(char)	198	,"%C6");    //	AE Digraph	
			val2 = charExpand(val2 ,(char)	199	,"%C7");    //	Ç (C Cedilla)
			val2 = charExpand(val2 ,(char)	200	,"%C8");    //	E Grave	
			val2 = charExpand(val2 ,(char)	201	,"%C9");    //	E Acute	
			val2 = charExpand(val2 ,(char)	202	,"%CA");    //	E Circumflex	
			val2 = charExpand(val2 ,(char)	203	,"%CB");    //	E Diaeresis	
			val2 = charExpand(val2 ,(char)	204	,"%CC");    //	I Grave	
			val2 = charExpand(val2 ,(char)	205	,"%CD");    //	I Acute	
			val2 = charExpand(val2 ,(char)	206	,"%CE");    //	I Circumflex	
			val2 = charExpand(val2 ,(char)	207	,"%CF");    //	I Diaeresis	
			val2 = charExpand(val2 ,(char)	208	,"%D0");    //	G Breve	
			val2 = charExpand(val2 ,(char)	209	,"%D1");    //	N Tilde	
			val2 = charExpand(val2 ,(char)	210	,"%D2");    //	O Grave	
			val2 = charExpand(val2 ,(char)	211	,"%D3");    //	O Acute	
			val2 = charExpand(val2 ,(char)	212	,"%D4");    //	O Circumflex	
			val2 = charExpand(val2 ,(char)	213	,"%D5");    //	O Tilde	
			val2 = charExpand(val2 ,(char)	214	,"%D6");    //	Ö (O Diaeresis)
			val2 = charExpand(val2 ,(char)	215	,"%D7");    //	Multiply (x)	
			val2 = charExpand(val2 ,(char)	216	,"%D8");    //	O Slash	
			val2 = charExpand(val2 ,(char)	217	,"%D9");    //	U Grave	
			val2 = charExpand(val2 ,(char)	218	,"%DA");    //	U Acute	
			val2 = charExpand(val2 ,(char)	219	,"%DB");    //	U Circumflex	
			val2 = charExpand(val2 ,(char)	220	,"%DC");    //	Ü (U Diaeresis)
			val2 = charExpand(val2 ,(char)	221	,"%DD");    //	I Dot Above	
			val2 = charExpand(val2 ,(char)	222	,"%DE");    //	S Cedilla	
			val2 = charExpand(val2 ,(char)	223	,"%DF");    //	German Double s	
			val2 = charExpand(val2 ,(char)	224	,"%E0");    //	a Grave	
			val2 = charExpand(val2 ,(char)	225	,"%E1");    //	a Acute	
			val2 = charExpand(val2 ,(char)	226	,"%E2");    //	a Circumflex	
			val2 = charExpand(val2 ,(char)	227	,"%E3");    //	a Tilde	
			val2 = charExpand(val2 ,(char)	228	,"%E4");    //	a Diaeresis	
			val2 = charExpand(val2 ,(char)	229	,"%E5");    //	a Ring	
			val2 = charExpand(val2 ,(char)	230	,"%E6");    //	ae Digraph	
			val2 = charExpand(val2 ,(char)	231	,"%E7");    //	ç (c Cedilla)
			val2 = charExpand(val2 ,(char)	232	,"%E8");    //	e Grave	
			val2 = charExpand(val2 ,(char)	233	,"%E9");    //	e Acute	
			val2 = charExpand(val2 ,(char)	234	,"%EA");    //	e Ogonek	
			val2 = charExpand(val2 ,(char)	235	,"%EB");    //	e Diaeresis	
			val2 = charExpand(val2 ,(char)	236	,"%EC");    //	e Dot Above	
			val2 = charExpand(val2 ,(char)	237	,"%ED");    //	i Acute	
			val2 = charExpand(val2 ,(char)	238	,"%EE");    //	i Circumflex	
			val2 = charExpand(val2 ,(char)	239	,"%EF");    //	i Macron	
			val2 = charExpand(val2 ,(char)	240	,"%F0");    //	g Breve	
			val2 = charExpand(val2 ,(char)	241	,"%F1");    //	n Tilde	
			val2 = charExpand(val2 ,(char)	242	,"%F2");    //	o Grave	
			val2 = charExpand(val2 ,(char)	243	,"%F3");    //	o Acute	
			val2 = charExpand(val2 ,(char)	244	,"%F4");    //	o Circumflex	
			val2 = charExpand(val2 ,(char)	245	,"%F5");    //	o Tilde	
			val2 = charExpand(val2 ,(char)	246	,"%F6");    //	ö (o Diaeresis)
			val2 = charExpand(val2 ,(char)	247	,"%F7");    //	Division	
			val2 = charExpand(val2 ,(char)	248	,"%F8");    //	o Slash	
			val2 = charExpand(val2 ,(char)	249	,"%F9");    //	u Grave	
			val2 = charExpand(val2 ,(char)	250	,"%FA");    //	u Acute	
			val2 = charExpand(val2 ,(char)	251	,"%FB");    //	u Circumflex	
			val2 = charExpand(val2 ,(char)	252	,"%FC");    //	ü (u Diaeresis)
			val2 = charExpand(val2 ,(char)	253	,"%FD");    //	Dotless i	
			val2 = charExpand(val2 ,(char)	254	,"%FE");    //	s Cedilla	
			val2 = charExpand(val2 ,(char)	255	,"%FF");    //	y Diaeresis	
						
			//bu dotnet icin onceden yapilmis,  epayapi 14 versiyonu icin yapilan degisikliklerin 
			//dotnet e  uygulanmis hali			
			*/

            val2 = charExpand(val2, (char)351, "%FE");    //	ş
            val2 = charExpand(val2, (char)254, "%FE");    //	s Cedilla	
            val2 = charExpand(val2, (char)222, "%DE");    //	S Cedilla

            val2 = charExpand(val2, (char)231, "%E7");    //	ç (c Cedilla)
            val2 = charExpand(val2, (char)199, "%C7");    //	Ç (C Cedilla)


            val2 = charExpand(val2, (char)287, "%F0");    //	ğ

            val2 = charExpand(val2, (char)350, "%DE");    //	Ş

            val2 = charExpand(val2, (char)286, "%D0");    //	Ğ

            val2 = charExpand(val2, (char)305, "%FD");    //	ı

            val2 = charExpand(val2, (char)304, "%DD");    //	İ
            val2 = charExpand(val2, (char)221, "%DD");    //	I Dot Above	

            val2 = charExpand(val2, (char)220, "%DC");    //	Ü (U Diaeresis)
            val2 = charExpand(val2, (char)252, "%FC");    //	ü (u Diaeresis)

            val2 = charExpand(val2, (char)246, "%F6");    //	ö (o Diaeresis)
            val2 = charExpand(val2, (char)214, "%D6");    //	Ö (O Diaeresis)

            return (val2);
        }
        private string charExpand(string intext, char ch, string newtext)
        {
            int i, j;
            char[] mybuf = new char[65000];
            j = 0;
            for (i = 0; i < intext.Length; i++)
            {
                if (intext[i].Equals(ch))
                {
                    for (int k = 0; k < newtext.Length; k++)
                    {
                        mybuf[j] = newtext[k];
                        j++;
                    }
                }
                else
                {
                    mybuf[j] = intext[i];
                    j++;
                }
            }

            //intext = mybuf.ToString();
            return (new string(mybuf, 0, j));
        }


        private string charExpandHigh(string intext)
        {
            int i, j;
            char[] mybuf = new char[65000];
            j = 0;
            for (i = 0; i < intext.Length; i++)
            {
                if (intext[i] > (char)255 &&
                        intext[i] != (char)351 && intext[i] != (char)287 &&
                        intext[i] != (char)350 && intext[i] != (char)286 &&
                        intext[i] != (char)305 && intext[i] != (char)304)
                //gelen karakter  255 den buyuk turkce degilse ters soru isareti yapalim
                {
                    mybuf[i] = (char)191;

                }
            }

            //intext = mybuf.ToString();
            return (new string(mybuf, 0, j));
        }



        public string processorder()
        {
            // Use the String builder object, more efficient than a string

            if (!configurated)
            {
                lock (configurationLockObject)
                {
                    if (!configurated)
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | (SecurityProtocolType)768 | (SecurityProtocolType)3072;

                        configurated = true;
                    }
                }
            }

            try
            {
                HttpWebRequest PayReq;
                Stream respStream;


                PayReq = (HttpWebRequest)WebRequest.Create(_Host);
                PayReq.Timeout = 90000;   // Set timeout for 90 seconds
                PayReq.Method = "POST";

                //Create all nodes.		
                AddNode("Name", _Name);
                AddNode("Password", _Password);
                AddNode("ClientId", _ClientId);
                AddNode("OkUrl", "https://bayi.kiracelektrik.com.tr/Siparis/ResultOrder");
                AddNode("FailUrl", "https://bayi.kiracelektrik.com.tr/Siparis/ResultOrder");

                AddNode("Email", _Email);
                if (_OrderResult == 0)
                    AddNode("Mode", "P");
                else
                    AddNode("Mode", "R");

                AddNode("OrderId", _Oid);
                AddNode("GroupId", _GroupId);
                AddNode("TransId", _TransId);
                AddNode("UserId", _UserId);
                AddNode("Type", _ChargeType);
                AddNode("Number", _CardNumber);
                if (_ExpMonth.Length != 0 && _ExpYear.Length != 0)
                    AddNode("Expires", _ExpMonth + "/" + _ExpYear);
                AddNode("Cvv2Val", _Cv2);

                if (_SubTotal.Length != 0)
                    if (Convert.ToDecimal(_SubTotal) > 0)
                    {
                        AddNode("Total", _SubTotal);
                        AddNode("Currency", _Currency);
                    }
                AddNode("Taksit", _Taksit);
                if (_OrderType != "")
                {
                    XmlNode nodeReq = ReqDoc.DocumentElement;
                    nodeReq.AppendChild(ReqDoc.CreateNode(XmlNodeType.Element, "PbOrder", ""));
                    AddToNode(ReqDoc, nodeReq["PbOrder"], "OrderType", _OrderType);
                    AddToNode(ReqDoc, nodeReq["PbOrder"], "TotalNumberPayments", _TotalNumberPayments);
                    AddToNode(ReqDoc, nodeReq["PbOrder"], "OrderFrequencyCycle", _OrderFrequencyCycle);
                    AddToNode(ReqDoc, nodeReq["PbOrder"], "OrderFrequencyInterval", _OrderFrequencyInterval);

                }
                //3d Secure
                AddNode("CardholderPresentCode", _CardholderPresentCode);
                AddNode("PayerSecurityLevel", _PayerSecurityLevel);
                AddNode("PayerTxnId", _PayerTxnId);
                AddNode("PayerAuthenticationCode", _PayerAuthenticationCode);
                //3d Secure

                AddNode("VersionInfo", "EPAYMENT-1.0.0.6");
                AddNode("IPAddress", _IP);
                AddNode("Comments", _Comments);

                //turkce icin
                char[] dummy = new char[80];

                XmlNode nodeBillTo = ReqDoc.DocumentElement;
                nodeBillTo.AppendChild(ReqDoc.CreateNode(XmlNodeType.Element, "BillTo", ""));
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "Name", _BName);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "Street1", _BAddr1);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "Street2", _BAddr2);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "Street3", _BAddr3);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "City", _BCity);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "PostalCode", _BZip);
                //AddToNode(ReqDoc,nodeBillTo["BillTo"],"Country",_BCountry);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "Company", _BCompany);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "TelVoice", _Phone);
                AddToNode(ReqDoc, nodeBillTo["BillTo"], "TelFax", _Fax);

                XmlNode nodeShipTo = ReqDoc.DocumentElement;
                nodeShipTo.AppendChild(ReqDoc.CreateNode(XmlNodeType.Element, "ShipTo", ""));

                AddToNode(ReqDoc, nodeShipTo["ShipTo"], "Name", _SName);
                AddToNode(ReqDoc, nodeShipTo["ShipTo"], "Street1", _SAddr1);
                AddToNode(ReqDoc, nodeShipTo["ShipTo"], "Street2", _SAddr2);
                AddToNode(ReqDoc, nodeShipTo["ShipTo"], "Street3", _SAddr3);


                AddToNode(ReqDoc, nodeShipTo["ShipTo"], "City", _SCity);
                //AddToNode(ReqDoc,nodeShipTo["ShipTo"],"StateProv",_SState);
                //AddToNode(ReqDoc,nodeShipTo["ShipTo"],"PostalCode",_SZip);
                //AddToNode(ReqDoc,nodeShipTo["ShipTo"],"Country",_SCountry);

                string postData = "DATA=" + ReqDoc.OuterXml;
                Encoding encoding = Encoding.GetEncoding("ISO-8859-9");

                byte[] byte1 = encoding.GetBytes(postData);
                // Set the content type of the data being posted.
                PayReq.ContentType = "application/x-www-form-urlencoded;charset=ISO-8859-9";
                // Set the content length of the string being posted.
                PayReq.ContentLength = postData.Length;

                Stream reqStream = PayReq.GetRequestStream();
                reqStream.Write(byte1, 0, byte1.Length);
                reqStream.Close();

                // Get the Response and insert in the Stream object
                XmlDocument doc = new XmlDocument();
                respStream = PayReq.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(respStream);
                StreamWriter writer = new StreamWriter(@"C:\test.txt");
                writer.WriteLine(reader.ReadToEnd());
                writer.Flush();
                writer.Close();
                XmlTextReader xreader = new XmlTextReader(respStream);
                xreader.MoveToContent();

                doc.Load(reader);
                XmlNode root = doc.FirstChild;

                try
                {
                    _Oid = root["OrderId"].InnerXml;
                }
                catch (Exception e)
                {
                    _Oid = "";
                }

                try
                {
                    _GroupId = root["GroupId"].InnerXml;
                }
                catch (Exception e)
                {
                    _GroupId = "";
                }

                try
                {
                    _ErrMsg = root["ErrMsg"].InnerXml;
                }
                catch (Exception e)
                {
                    _ErrMsg = "";
                }

                try
                {
                    _Appr = root["Response"].InnerXml;
                }
                catch (Exception e)
                {
                    _Appr = "";
                }

                try
                {
                    _Code = root["AuthCode"].InnerXml;
                }
                catch (Exception e)
                {
                    _Code = "";
                }

                try
                {
                    _RefNo = root["HostRefNum"].InnerXml;
                }
                catch (Exception e)
                {
                    _RefNo = "";
                }

                try
                {
                    _ProcReturnCode = root["ProcReturnCode"].InnerXml;
                }
                catch (Exception e)
                {
                    _ProcReturnCode = "";
                }

                try
                {
                    _TransId = root["TransId"].InnerXml;
                }
                catch (Exception e)
                {
                    _TransId = "";
                }

                try
                {
                    ExtraDoc.LoadXml(root["Extra"].OuterXml);
                }
                catch (Exception e)
                {
                    ExtraDoc.LoadXml("<Extra></Extra>");
                }

                //_ErrMsg=((int)_BName[0]).ToString()+"||"+((int)_BName[1]).ToString()+"||"+((int)_BName[2]).ToString()+"||";

            }
            catch (Exception e)
            {
                //_ErrMsg = e.Message;
                _ErrMsg = e.StackTrace;
                //_ErrMsg = ReqRoot.OuterXml;
                //_ErrMsg=((int)_BName[0]).ToString()+"||"+((int)_BName[1]).ToString()+"||"+((int)_BName[2]).ToString()+"||";

                return "0";
            }

            return "1";     //OK
        }

        //input values

        public void addpborder(string OrderT, string TotalNumberP, string OrderFrequencyC, string OrderFrequencyI)
        {
            _OrderType = OrderT;
            _TotalNumberPayments = TotalNumberP;
            _OrderFrequencyCycle = OrderFrequencyC;
            _OrderFrequencyInterval = OrderFrequencyI;
        }

        public string Extra(string fname)
        {
            try
            {
                _FNameExtra = fname;
                if (ExtraDoc != null)
                {
                    if (ExtraDoc.HasChildNodes)
                    {
                        XmlNode ExtraRoot = ExtraDoc.FirstChild;
                        if (ExtraRoot != null)
                            try
                            {
                                _FValueExtra = ExtraRoot[_FNameExtra].InnerText;
                                //_FValueExtra = "Inner Text is NULL";
                            }
                            catch (Exception e)
                            {
                                _FValueExtra = "";
                            }
                    }
                    else
                    {
                        //_FValueExtra = "EXTRA HAS NO CHILD NODES"+ExtraDoc.OuterXml;
                        _FValueExtra = "";
                    }
                }
                else
                {
                    //_FValueExtra = "ExtraDoc is null";
                    _FValueExtra = "";
                }

                if (_FValueExtra == null)
                    _FValueExtra = "";

                return (_FValueExtra);

            }
            catch (Exception e)
            {
                _ErrMsg = e.Message;

                return "";
            }

        }

        public void putExtra(string fname, string fvalue)
        {
            string _FName = fname;
            string _FValue = fvalue;
            XmlNode AddItemRoot = ReqDoc.DocumentElement["Extra"];
            AddToNode(ReqDoc, AddItemRoot, _FName, _FValue);
        }
        public void addItem(string id, string desc, string price, string quantity,
            string itemnumber, string productcode, string total)
        {
            string _Id = id;
            string _Desc = desc;
            string _Price = price;
            string _Quantity = quantity;
            string _ItemNumber = itemnumber;
            string _ProductCode = productcode;
            string _Total = total;

            XmlNode AddItemRoot = ReqDoc.DocumentElement["OrderItemList"];
            XmlNode AddItemOrder = ReqDoc.CreateNode(XmlNodeType.Element, "OrderItem", "");
            AddItemRoot.AppendChild(AddItemOrder);


            if (Convert.ToDecimal(_ItemNumber) > 0)
                AddToNode(ReqDoc, AddItemOrder, "ItemNumber", _ItemNumber);
            else
                AddToNode(ReqDoc, AddItemOrder, "ItemNumber", Convert.ToString(AddItemRoot.ChildNodes.Count));

            AddToNode(ReqDoc, AddItemOrder, "ProductCode", _ProductCode);

            AddToNode(ReqDoc, AddItemOrder, "Qty", _Quantity);
            AddToNode(ReqDoc, AddItemOrder, "Desc", _Desc);
            AddToNode(ReqDoc, AddItemOrder, "Id", _Id);
            AddToNode(ReqDoc, AddItemOrder, "Price", _Price);
            AddToNode(ReqDoc, AddItemOrder, "Total", _Total);

        }
        public string host
        {
            set
            {
                _Host = value;
            }
        }
        public string name
        {
            set
            {
                _Name = value;
            }
        }
        public string password
        {
            set
            {
                _Password = value;
            }
        }
        public string clientid
        {
            set
            {
                _ClientId = value;
            }
        }
        public string expmonth
        {
            set
            {
                _ExpMonth = value;
            }
        }
        public string expyear
        {
            set
            {
                _ExpYear = value;
            }
        }
        public string cv2
        {
            set
            {
                _Cv2 = value;
            }
        }
        public string subtotal
        {
            set
            {
                _SubTotal = value;
            }
        }
        public string currency
        {
            set
            {
                _Currency = value;
            }
        }
        public string chargetype
        {
            set
            {
                _ChargeType = value;
            }
        }

        //return values
        public string errmsg
        {
            get
            {
                return _ErrMsg;
            }
        }
        public string oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                _Oid = value;
            }
        }

        public string code
        {
            get
            {
                return _Code;
            }
        }

        public string refno
        {
            get
            {
                return _RefNo;
            }
        }
        public string groupid
        {
            get
            {
                return _GroupId;
            }
            set
            {
                _GroupId = value;
            }
        }
        public string transid
        {
            get
            {
                return _TransId;
            }
        }
        public string total
        {
            get
            {
                return _Total;
            }
        }
        public string taxtotal
        {
            set
            {
                _TaxTotal = value;
            }
        }

        public string shiptotal
        {
            set
            {
                _ShipTotal = value;
            }
        }
        public string ordno
        {
            get
            {
                return _Ordno;
            }
        }
        public string err
        {
            get
            {
                return _Err;
            }
        }
        public string time
        {
            get
            {
                return _Time;
            }
        }
        public string procreturncode
        {
            get
            {
                return _ProcReturnCode;
            }
        }


        public string appr
        {
            get
            {
                return _Appr;
            }
        }
        public string result
        {
            set
            {
                _Result = value;
            }
        }
        public string taksit
        {
            set
            {
                _Taksit = value;
            }
        }
        public string comments
        {
            set
            {
                _Comments = value;
            }
        }
        public string sstate
        {
            set
            {
                _SState = value;
            }
        }
        public string bstate
        {
            set
            {
                _BState = value;
            }
        }
        public string bcompany
        {
            set
            {
                _BCompany = value;
            }
        }
        public string refernum
        {
            set
            {
                _ReferNum = value;
            }
        }
        public string fax
        {
            set
            {
                _Fax = value;
            }
        }
        public string ip
        {
            set
            {
                _IP = value;
            }
        }
        public string port
        {
            set
            {
                _Port = value;
            }
        }
        public string userid
        {
            set
            {
                _UserId = value;
            }
        }
        public int orderresult
        {
            set
            {
                _OrderResult = value;
            }
        }
        public string scountry
        {
            set
            {
                _SCountry = value;
            }
        }
        public string szip
        {
            set
            {
                _SZip = value;
            }
        }
        public string scity
        {
            set
            {
                _SCity = value;
            }
        }
        public string saddr3
        {
            set
            {
                _SAddr3 = value;
            }
        }
        public string saddr2
        {
            set
            {
                _SAddr2 = value;
            }
        }
        public string saddr1
        {
            set
            {
                _SAddr1 = value;
            }
        }
        public string sname
        {
            set
            {
                _SName = value;
            }
        }
        public string bcountry
        {
            set
            {
                _BCountry = value;
            }
        }
        public string bzip
        {
            set
            {
                _BZip = value;
            }
        }
        public string bcity
        {
            set
            {
                _BCity = value;
            }
        }
        public string baddr3
        {
            set
            {
                _BAddr3 = value;
            }
        }
        public string baddr2
        {
            set
            {
                _BAddr2 = value;
            }
        }
        public string baddr1
        {
            set
            {
                _BAddr1 = value;
            }
        }
        public string bname
        {
            set
            {
                _BName = value;
            }
        }
        public string phone
        {
            set
            {
                _Phone = value;
            }
        }
        public string email
        {
            set
            {
                _Email = value;
            }
        }
        public string cardnumber
        {
            set
            {
                _CardNumber = value;
            }
        }
        //3dsecure
        public string cardholderpresentcode
        {
            set
            {
                _CardholderPresentCode = value;
            }
        }
        public string payersecuritylevel
        {
            set
            {
                _PayerSecurityLevel = value;
            }
        }
        public string payerauthenticationcode
        {
            set
            {
                _PayerAuthenticationCode = value;
            }
        }
        public string payertxnid
        {
            set
            {
                _PayerTxnId = value;
            }
        }
        //3dsecure
    }
}
