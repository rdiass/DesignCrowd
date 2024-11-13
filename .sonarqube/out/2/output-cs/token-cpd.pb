ß	
-C:\Dev\DesignCrowd\DesignCrowd.Api\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddApiConfiguration $
($ %
)% &
;& '
builder 
. 
Services 
. #
AddSwaggerConfiguration (
(( )
)) *
;* +
builder 
. 
Services 
. 
RegisterServices !
(! "
)" #
;# $
var

 
app

 
=

 	
builder


 
.

 
Build

 
(

 
)

 
;

 
app 
. #
UseSwaggerConfiguration 
( 
) 
; 
app 
. 
UseApiConfiguration 
( 
) 
; 
app 
. 
Run 
( 
) 	
;	 

public 
partial 
class 
Program 
{ 
}  ò
NC:\Dev\DesignCrowd\DesignCrowd.Api\Controllers\BusinessDayCounterController.cs
	namespace 	
DesignCrowd
 
. 
Api 
. 
Controllers %
;% &
[ 
ApiController 
] 
[ 
Route 
( 
$str 
) 
] 
public 
class (
BusinessDayCounterController )
:* +
ControllerBase, :
{		 
private

 
readonly

 
ILogger

 
<

 (
BusinessDayCounterController

 9
>

9 :
_logger

; B
;

B C
private 
readonly &
IBusinessDayCounterService /&
_businessDayCounterService0 J
;J K
public 
(
BusinessDayCounterController '
(' (
ILogger( /
</ 0(
BusinessDayCounterController0 L
>L M
loggerN T
,T U&
IBusinessDayCounterServiceV p
businessDayCounter	q ƒ
)
ƒ „
{ 
_logger 
= 
logger 
; &
_businessDayCounterService "
=# $
businessDayCounter% 7
;7 8
} 
[ 
HttpGet 
( 
Name 
= 
$str -
)- .
]. /
[  
ProducesResponseType 
( 
typeof  
(  !
int! $
)$ %
,% &
StatusCodes' 2
.2 3
Status200OK3 >
)> ?
]? @
[  
ProducesResponseType 
( 
StatusCodes %
.% &
Status404NotFound& 7
)7 8
]8 9
[  
ProducesResponseType 
( 
StatusCodes %
.% &(
Status500InternalServerError& B
)B C
]C D
public 

IActionResult &
GetWeekdaysBetweenTwoDates 3
(3 4
DateTime4 <
	firstDate= F
,F G
DateTimeH P

secondDateQ [
)[ \
{ 
try 
{ 	
if 
( 

secondDate 
<= 
	firstDate '
)' (
{ 
_logger 
. 
LogError  
(  !
$str! 5
)5 6
;6 7
return 
Problem 
( 
$str B
,B C

statusCodeD N
:N O
StatusCodesP [
.[ \
Status400BadRequest\ o
)o p
;p q
} 
int   
businessDays   
=   &
_businessDayCounterService   9
.  9 :#
WeekdaysBetweenTwoDates  : Q
(  Q R
	firstDate  R [
,  [ \

secondDate  ] g
)  g h
;  h i
return!! 
Ok!! 
(!! 
businessDays!! "
)!!" #
;!!# $
}"" 	
catch## 
(## 
	Exception## 
ex## 
)## 
{$$ 	
_logger%% 
.%% 
LogError%% 
(%% 
ex%% 
,%%  
$str%%! ?
)%%? @
;%%@ A
return&& 
Problem&& 
(&& 
$str&& 9
,&&9 :

statusCode&&; E
:&&E F
StatusCodes&&G R
.&&R S(
Status500InternalServerError&&S o
)&&o p
;&&p q
}'' 	
}(( 
})) Û
AC:\Dev\DesignCrowd\DesignCrowd.Api\Configuration\SwaggerConfig.cs
	namespace 	
DesignCrowd
 
. 
Api 
. 
Configuration '
;' (
public 
static 
class 
SwaggerConfig !
{ 
public 

static 
IServiceCollection $#
AddSwaggerConfiguration% <
(< =
this= A
IServiceCollectionB T
servicesU ]
)] ^
{ 
services		 
.		 
AddSwaggerGen		 
(		 
c		  
=>		! #
{

 	
c 
. 

SwaggerDoc 
( 
$str 
, 
new "
(" #
)# $
{ 
Version 
= 
$str 
, 
Title 
= 
$str )
,) *
Description 
= 
$str W
,W X
Contact 
= 
new 
( 
) 
{  !
Name" &
=' (
$str) 8
,8 9
Email: ?
=@ A
$strB \
}] ^
} 
) 
; 
} 	
)	 

;
 
return 
services 
; 
} 
public 

static 
IApplicationBuilder %#
UseSwaggerConfiguration& =
(= >
this> B
IApplicationBuilderC V
appW Z
)Z [
{ 
app 
. 

UseSwagger 
( 
) 
; 
app 
. 
UseSwaggerUI 
( 
c 
=> 
{ 	
c 
. 
SwaggerEndpoint 
( 
$str 8
,8 9
$str: >
)> ?
;? @
} 	
)	 

;
 
return 
app 
; 
}   
}!! ì
MC:\Dev\DesignCrowd\DesignCrowd.Api\Configuration\DependencyInjectionConfig.cs
	namespace 	
DesignCrowd
 
. 
Api 
. 
Configuration '
;' (
public 
static 
class %
DependencyInjectionConfig -
{ 
public		 

static		 
void		 
RegisterServices		 '
(		' (
this		( ,
IServiceCollection		- ?
services		@ H
)		H I
{

 
services 
. 
	AddScoped 
< &
IBusinessDayCounterService 5
,5 6%
BusinessDayCounterService7 P
>P Q
(Q R
)R S
;S T
services 
. 
	AddScoped 
< !
IPublicHolidayFactory 0
,0 1 
PublicHolidayFactory2 F
>F G
(G H
)H I
;I J
} 
} ÷
=C:\Dev\DesignCrowd\DesignCrowd.Api\Configuration\ApiConfig.cs
	namespace 	
DesignCrowd
 
. 
Api 
. 
Configuration '
;' (
public 
static 
class 
	ApiConfig 
{ 
public 

static 
IServiceCollection $
AddApiConfiguration% 8
(8 9
this9 =
IServiceCollection> P
servicesQ Y
)Y Z
{ 
services 
. 
AddControllers 
(  
)  !
.! "
AddJsonOptions" 0
(0 1
options1 8
=>9 ;
{ 	
options		 
.		 !
JsonSerializerOptions		 )
.		) *'
PropertyNameCaseInsensitive		* E
=		F G
false		H M
;		M N
}

 	
)

	 

;


 
return 
services 
; 
} 
public 

static 
IApplicationBuilder %
UseApiConfiguration& 9
(9 :
this: >
IApplicationBuilder? R
appS V
)V W
{ 
app 
. 
UseHttpsRedirection 
(  
)  !
;! "
app 
. 

UseRouting 
( 
) 
; 
app 
. 
UseAuthorization 
( 
) 
; 
app 
. 
UseEndpoints 
( 
	endpoints "
=># %
{ 	
	endpoints 
. 
MapControllers $
($ %
)% &
;& '
} 	
)	 

;
 
return 
app 
; 
} 
} 