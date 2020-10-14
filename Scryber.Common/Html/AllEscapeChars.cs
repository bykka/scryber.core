﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scryber.Html
{
    public static class AllEscapeChars
    {
        public static string AllKnown = 
@"™ &#8482;   
€   &euro;
! &#33;   
"" &#34; &quot; 
# &#35;   
$ &#36;   
% &#37;   
& &#38; &amp; 
' &#39;   
( &#40;   
) &#41;   
* &#42;   
+ &#43;   
, &#44;   
- &#45;   
. &#46;   
/ &#47;   
0 &#48;   
1 &#49;   
2 &#50;   
3 &#51;   
4 &#52;   
5 &#53;   
6 &#54;   
7 &#55;   
8 &#56;   
9 &#57;   
: &#58;   
; &#59;   
< &#60; &lt; 
= &#61;   
> &#62; &gt; 
? &#63;   
@ &#64;   
A &#65;   
B &#66;   
C &#67;   
D &#68;   
E &#69;   
F &#70;   
G &#71;   
H &#72;   
I &#73;   
J &#74;   
K &#75;   
L &#76;   
M &#77;   
N &#78;   
O &#79;   
P &#80;   
Q &#81;   
R &#82;   
S &#83;   
T &#84;   
U &#85;   
V &#86;   
W &#87;   
X &#88;   
Y &#89;   
Z &#90;   
[ &#91;   
\ &#92;   
] &#93;   
^ &#94;   
_ &#95;   
` &#96;   
a &#97;   
b &#98;   
c &#99;   
d &#100;   
e &#101;   
f &#102;   
g &#103;   
h &#104;   
i &#105;   
j &#106;   
k &#107;   
l &#108;   
m &#109;   
n &#110;   
o &#111;   
p &#112;   
q &#113;   
r &#114;   
s &#115;   
t &#116;   
u &#117;   
v &#118;   
w &#119;   
x &#120;   
y &#121;   
z &#122;   
{ &#123;   
| &#124;   
} &#125;   
~ &#126;   
¡ &#161; &iexcl; 
¢ &#162; &cent; 
£ &#163; &pound; 
¤ &#164; &curren; 
¥ &#165; &yen; 
¦ &#166; &brvbar; 
§ &#167; &sect; 
¨ &#168; &uml; 
© &#169; &copy; 
ª &#170; &ordf; 
« &#171;   
¬ &#172; &not; 
® &#174; &reg; 
¯ &#175; &macr; 
° &#176; &deg; 
± &#177; &plusmn; 
² &#178; &sup2; 
³ &#179; &sup3; 
´ &#180; &acute; 
µ &#181; &micro; 
¶ &#182; &para; 
· &#183; &middot; 
¸ &#184; &cedil; 
¹ &#185; &sup1; 
º &#186; &ordm; 
» &#187; &raquo; 
¼ &#188; &frac14; 
½ &#189; &frac12; 
¾ &#190; &frac34; 
¿ &#191; &iquest; 
À &#192; &Agrave; 
Á &#193; &Aacute; 
Â &#194; &Acirc; 
Ã &#195; &Atilde; 
Ä &#196; &Auml; 
Å &#197 &Aring; 
Æ &#198; &AElig; 
Ç &#199; &Ccedil; 
È &#200; &Egrave; 
É &#201; &Eacute; 
Ê &#202; &Ecirc; 
Ë &#203; &Euml; 
Ì &#204; &Igrave; 
Í &#205; &Iacute; 
Î &#206; &Icirc; 
Ï &#207; &Iuml; 
Ð &#208; &ETH; 
Ñ &#209; &Ntilde; 
Ò &#210; &Ograve; 
Ó &#211; &Oacute; 
Ô &#212; &Ocirc; 
Õ &#213; &Otilde; 
Ö &#214; &Ouml; 
× &#215; &times; 
Ø &#216; &Oslash; 
Ù &#217; &Ugrave; 
Ú &#218; &Uacute; 
Û &#219; &Ucirc; 
Ü &#220; &Uuml; 
Ý &#221; &Yacute; 
Þ &#222; &THORN; 
ß &#223; &szlig; 
à &#224; &agrave; 
á &#225; &aacute; 
â &#226; &acirc; 
ã &#227; &atilde; 
ä &#228; &auml; 
å &#229; &aring; 
æ &#230; &aelig; 
ç &#231; &ccedil; 
è &#232; &egrave; 
é &#233; &eacute; 
ê &#234; &ecirc; 
ë &#235; &euml; 
ì &#236; &igrave; 
í &#237 &iacute; 
î &#238; &icirc; 
ï &#239; &iuml; 
ð &#240; &eth; 
ñ &#241; &ntilde; 
ò &#242; &ograve; 
ó &#243; &oacute; 
ô &#244; &ocirc; 
õ &#245; &otilde; 
ö &#246; &ouml; 
÷ &#247; &divide; 
ø &#248; &oslash; 
ù &#249; &ugrave; 
ú &#250; &uacute; 
û &#251; &ucirc; 
ü &#252; &uuml; 
ý &#253; &yacute; 
þ &#254; &thorn; 
ÿ &#255;   
Ā &#256;   
ā &#257;   
Ă &#258;   
ă &#259;   
Ą &#260;   
ą &#261;   
Ć &#262;   
ć &#263;   
Ĉ &#264;   
ĉ &#265;   
Ċ &#266;   
ċ &#267;   
Č &#268;   
č &#269;   
Ď &#270;   
ď &#271;   
Đ &#272;   
đ &#273;   
Ē &#274;   
ē &#275;   
Ĕ &#276;   
ĕ &#277   
Ė &#278;   
ė &#279;   
Ę &#280;   
ę &#281;   
Ě &#282;   
ě &#283;   
Ĝ &#284;   
ĝ &#285;   
Ğ &#286;   
ğ &#287;   
Ġ &#288;   
ġ &#289;   
Ģ &#290;   
ģ &#291;   
Ĥ &#292;   
ĥ &#293;   
Ħ &#294;   
ħ &#295;   
Ĩ &#296;   
ĩ &#297;   
Ī &#298;   
ī &#299;   
Ĭ &#300;   
ĭ &#301;   
Į &#302;   
į &#303;   
İ &#304;   
ı &#305;   
Ĳ &#306;   
ĳ &#307;   
Ĵ &#308;   
ĵ &#309;   
Ķ &#310;   
ķ &#311;   
ĸ &#312;   
Ĺ &#313;   
ĺ &#314;   
Ļ &#315;   
ļ &#316;   
Ľ &#317   
ľ &#318;   
Ŀ &#319;   
ŀ &#320;   
Ł &#321;   
ł &#322;   
Ń &#323;   
ń &#324;   
Ņ &#325;   
ņ &#326;   
Ň &#327;   
ň &#328;   
ŉ &#329;   
Ŋ &#330;   
ŋ &#331;   
Ō &#332;   
ō &#333;   
Ŏ &#334;   
ŏ &#335;   
Ő &#336;   
ő &#337;   
Œ &#338;   
œ &#339;   
Ŕ &#340;   
ŕ &#341;   
Ŗ &#342;   
ŗ &#343;   
Ř &#344;   
ř &#345;   
Ś &#346;   
ś &#347;   
Ŝ &#348;   
ŝ &#349;   
Ş &#350;   
ş &#351;   
Š &#352;   
š &#353;   
Ţ &#354;   
ţ &#355;   
Ť &#356;   
ť &#357   
Ŧ &#358;   
ŧ &#359;   
Ũ &#360;   
ũ &#361;   
Ū &#362;   
ū &#363;   
Ŭ &#364;   
ŭ &#365;   
Ů &#366;   
ů &#367;   
Ű &#368;   
ű &#369;   
Ų &#370;   
ų &#371;   
Ŵ &#372;   
ŵ &#373;   
Ŷ &#374;   
ŷ &#375;   
Ÿ &#376;   
Ź &#377;   
ź &#378;   
Ż &#379;   
ż &#380;   
Ž &#381;   
ž &#382;   
ſ &#383;   
Ŕ &#340;   
ŕ &#341;   
Ŗ &#342;   
ŗ &#343;   
Ř &#344;   
ř &#345;   
Ś &#346;   
ś &#347;   
Ŝ &#348;   
ŝ &#349;   
Ş &#350;   
ş &#351;   
Š &#352;   
š &#353;   
Ţ &#354;   
ţ &#355;   
Ť &#356;   
ť &#577;   
Ŧ &#358;   
ŧ &#359;   
Ũ &#360;   
ũ &#361;   
Ū &#362;   
ū &#363;   
Ŭ &#364;   
ŭ &#365;   
Ů &#366;   
ů &#367;   
Ű &#368;   
ű &#369;   
Ų &#370;   
ų &#371;   
Ŵ &#372;   
ŵ &#373;   
Ŷ &#374;   
ŷ &#375;   
Ÿ &#376;   
Ź &#377   
ź &#378;   
Ż &#379;   
ż &#380;   
Ž &#381;   
ž &#382;   
ſ &#383;   
";
    }
}