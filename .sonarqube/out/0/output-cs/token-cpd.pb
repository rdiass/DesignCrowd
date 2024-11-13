«
AC:\Dev\DesignCrowd\DesignCrowd.Data\Models\FloatingHolidayRule.cs
	namespace 	
DesignCrowd
 
. 
Data 
. 
Models !
;! "
public 
class 
FloatingHolidayRule  
:! "
PublicHolidayRule# 4
{ 
private 
readonly 
	DayOfWeek 

_dayOfWeek )
;) *
private 
readonly 
int 
_month 
;  
private		 
readonly		 
int		 
_weekOccurrence		 (
;		( )
public 

FloatingHolidayRule 
( 
	DayOfWeek (
	dayOfWeek) 2
,2 3
int4 7
month8 =
,= >
int? B
weekOccurrenceC Q
)Q R
{ 

_dayOfWeek 
= 
	dayOfWeek 
; 
_month 
= 
month 
; 
_weekOccurrence 
= 
weekOccurrence (
;( )
} 
public 

override 
bool 
	IsHoliday "
(" #
DateTime# +
date, 0
)0 1
{ 
return 
date 
. 
	DayOfWeek 
==  

_dayOfWeek! +
&&, .
date 
. 
Month 
== 
_month #
&&$ &
GetWeekOccurrence  
(  !
date! %
)% &
==' )
_weekOccurrence* 9
;9 :
} 
private 
int 
GetWeekOccurrence !
(! "
DateTime" *
date+ /
)/ 0
{ 
int 
firstDayOfMonth 
= 
( 
int "
)" #
new# &
DateTime' /
(/ 0
date0 4
.4 5
Year5 9
,9 :
date; ?
.? @
Month@ E
,E F
$numG H
)H I
.I J
	DayOfWeekJ S
;S T
int 

dayOfMonth 
= 
date 
. 
Day !
;! "
return 
( 

dayOfMonth 
+ 
( 
$num 
-  !
firstDayOfMonth" 1
)1 2
)2 3
/4 5
$num6 7
;7 8
} 
} Ü
BC:\Dev\DesignCrowd\DesignCrowd.Data\Models\FixedDateHolidayRule.cs
	namespace 	
DesignCrowd
 
. 
Data 
. 
Models !
;! "
public 
class  
FixedDateHolidayRule !
:" #
PublicHolidayRule$ 5
{ 
private 
readonly 
DateTime 

_fixedDate (
;( )
public		 
 
FixedDateHolidayRule		 
(		  
DateTime		  (
	fixedDate		) 2
)		2 3
{

 

_fixedDate 
= 
	fixedDate 
; 
} 
public 

override 
bool 
	IsHoliday "
(" #
DateTime# +
date, 0
)0 1
=>2 4
date5 9
.9 :
Date: >
==? A

_fixedDateB L
.L M
DateM Q
;Q R
} ©
CC:\Dev\DesignCrowd\DesignCrowd.Data\Factory\PublicHolidayFactory.cs
	namespace 	
DesignCrowd
 
. 
Data 
. 
Factory "
;" #
public 
class  
PublicHolidayFactory !
:" #!
IPublicHolidayFactory$ 9
{ 
public 

PublicHolidayRule 
CreateHolidayRule .
(. /
string/ 5
ruleType6 >
,> ?
DateTime@ H
?H I
	fixedDateJ S
=T U
nullV Z
,Z [
	DayOfWeek\ e
?e f
	dayOfWeekg p
=q r
nulls w
,w x
inty |
?| }
month	~ ƒ
=
„ …
null
† Š
,
Š ‹
int
Œ 
?
 
weekOccurrence
‘ Ÿ
=
  ¡
null
¢ ¦
)
¦ §
{		 
switch

 
(

 
ruleType

 
)

 
{ 	
case 
$str 
: 
return 
new  
FixedDateHolidayRule /
(/ 0
	fixedDate0 9
.9 :
Value: ?
)? @
;@ A
case 
$str 
: 
return 
new 
FloatingHolidayRule .
(. /
	dayOfWeek/ 8
.8 9
Value9 >
,> ?
month@ E
.E F
ValueF K
,K L
weekOccurrenceM [
.[ \
Value\ a
)a b
;b c
default 
: 
throw 
new 
ArgumentException +
(+ ,
$str, ?
)? @
;@ A
} 	
} 
} ü
DC:\Dev\DesignCrowd\DesignCrowd.Data\Factory\IPublicHolidayFactory.cs
	namespace 	
DesignCrowd
 
. 
Data 
. 
Factory "
;" #
public 
	interface !
IPublicHolidayFactory &
{ 
PublicHolidayRule 
CreateHolidayRule '
(' (
string( .
ruleType/ 7
,7 8
DateTime9 A
?A B
	fixedDateC L
=M N
nullO S
,S T
	DayOfWeekU ^
?^ _
	dayOfWeek` i
=j k
nulll p
,p q
intr u
?u v
monthw |
=} ~
null	 ƒ
,
ƒ „
int
… ˆ
?
ˆ ‰
weekOccurrence
Š ˜
=
™ š
null
› Ÿ
)
Ÿ  
;
  ¡
} è
DC:\Dev\DesignCrowd\DesignCrowd.Data\Abstraction\PublicHolidayRule.cs
	namespace 	
DesignCrowd
 
. 
Data 
. 
Abstraction &
;& '
public 
abstract 
class 
PublicHolidayRule '
{ 
public 

abstract 
bool 
	IsHoliday "
(" #
DateTime# +
date, 0
)0 1
;1 2
}		 