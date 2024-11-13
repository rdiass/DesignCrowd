Ù0
MC:\Dev\DesignCrowd\DesignCrowd.Business\Services\BusinessDayCounterService.cs
	namespace 	
DesignCrowd
 
. 
Business 
. 
Services '
;' (
public 
class %
BusinessDayCounterService &
:' (&
IBusinessDayCounterService) C
{ 
private		 
readonly		 
ILogger		 
<		 %
BusinessDayCounterService		 6
>		6 7
_logger		8 ?
;		? @
public 
%
BusinessDayCounterService $
($ %
ILogger% ,
<, -%
BusinessDayCounterService- F
>F G
loggerH N
)N O
{ 
_logger 
= 
logger 
; 
} 
public 

int #
WeekdaysBetweenTwoDates &
(& '
DateTime' /
	firstDate0 9
,9 :
DateTime; C

secondDateD N
)N O
{ 
if 

( 

secondDate 
<= 
	firstDate #
)# $
{ 	
return 
$num 
; 
} 	
int 
weekdays 
= 
$num 
; 
for 
( 
var 
date 
= 
	firstDate !
.! "
AddDays" )
() *
$num* +
)+ ,
;, -
date. 2
<3 4

secondDate5 ?
;? @
dateA E
=F G
dateH L
.L M
AddDaysM T
(T U
$numU V
)V W
)W X
{ 	
if 
( 
IsBusinessDay 
( 
date "
)" #
)# $
{ 
weekdays 
++ 
; 
} 
} 	
return!! 
weekdays!! 
;!! 
}"" 
public$$ 

int$$ '
BusinessDaysBetweenTwoDates$$ *
($$* +
DateTime$$+ 3
	firstDate$$4 =
,$$= >
DateTime$$? G

secondDate$$H R
,$$R S
IList$$T Y
<$$Y Z
DateTime$$Z b
>$$b c
publicHolidays$$d r
)$$r s
{%% 
int'' 
weekdays'' 
='' #
WeekdaysBetweenTwoDates'' .
(''. /
	firstDate''/ 8
,''8 9

secondDate'': D
)''D E
;''E F
foreach** 
(** 
var** 
holiday** 
in** 
publicHolidays**  .
)**. /
{++ 	
if,, 
(,, 
holiday,, 
>,, 
	firstDate,, #
&&,,$ &
holiday,,' .
<,,/ 0

secondDate,,1 ;
&&,,< >
IsBusinessDay,,? L
(,,L M
holiday,,M T
),,T U
),,U V
{-- 
weekdays.. 
--.. 
;.. 
}// 
}00 	
return22 
weekdays22 
;22 
}33 
public55 

int55 '
BusinessDaysBetweenTwoDates55 *
(55* +
DateTime55+ 3
	startDate554 =
,55= >
DateTime55? G
endDate55H O
,55O P
IEnumerable55Q \
<55\ ]
PublicHolidayRule55] n
>55n o
holidayRules55p |
)55| }
{66 
if77 

(77 
endDate77 
<=77 
	startDate77  
)77  !
{88 	
return99 
$num99 
;99 
}:: 	
int<< 
	totalDays<< 
=<< 
(<< 
endDate<<  
-<<! "
	startDate<<# ,
)<<, -
.<<- .
Days<<. 2
-<<3 4
$num<<5 6
;<<6 7
int== 
businessDays== 
=== 
$num== 
;== 
for?? 
(?? 
DateTime?? 
date?? 
=?? 
	startDate?? &
.??& '
AddDays??' .
(??. /
$num??/ 0
)??0 1
;??1 2
date??3 7
<??8 9
endDate??: A
;??A B
date??C G
=??H I
date??J N
.??N O
AddDays??O V
(??V W
$num??W X
)??X Y
)??Y Z
{@@ 	
ifAA 
(AA 
IsBusinessDayAA 
(AA 
dateAA "
,AA" #
holidayRulesAA$ 0
)AA0 1
)AA1 2
{BB 
businessDaysCC 
++CC 
;CC 
}DD 
}EE 	
returnGG 
businessDaysGG 
;GG 
}HH 
privateJJ 
staticJJ 
boolJJ 
IsBusinessDayJJ %
(JJ% &
DateTimeJJ& .
dateJJ/ 3
,JJ3 4
IEnumerableJJ5 @
<JJ@ A
PublicHolidayRuleJJA R
>JJR S
holidayRulesJJT `
)JJ` a
{KK 
returnLL 
dateLL 
.LL 
	DayOfWeekLL 
!=LL  
	DayOfWeekLL! *
.LL* +
SaturdayLL+ 3
&&LL4 6
dateMM 
.MM 
	DayOfWeekMM 
!=MM  
	DayOfWeekMM! *
.MM* +
SundayMM+ 1
&&MM2 4
!NN 
holidayRulesNN 
.NN 
AnyNN  
(NN  !
ruleNN! %
=>NN& (
ruleNN) -
.NN- .
	IsHolidayNN. 7
(NN7 8
dateNN8 <
)NN< =
)NN= >
;NN> ?
}OO 
privateQQ 
staticQQ 
boolQQ 
IsBusinessDayQQ %
(QQ% &
DateTimeQQ& .
dateQQ/ 3
)QQ3 4
{RR 
returnSS 
dateSS 
.SS 
	DayOfWeekSS 
!=SS  
	DayOfWeekSS! *
.SS* +
SaturdaySS+ 3
&&SS4 6
dateTT 
.TT 
	DayOfWeekTT 
!=TT  
	DayOfWeekTT! *
.TT* +
SundayTT+ 1
;TT1 2
}UU 
}VV Ö
PC:\Dev\DesignCrowd\DesignCrowd.Business\Interfaces\IBusinessDayCounterService.cs
	namespace 	
DesignCrowd
 
. 
Business 
. 

Interfaces )
;) *
public 
	interface &
IBusinessDayCounterService +
{ 
int #
WeekdaysBetweenTwoDates 
(  
DateTime  (
	firstDate) 2
,2 3
DateTime4 <

secondDate= G
)G H
;H I
int '
BusinessDaysBetweenTwoDates #
(# $
DateTime$ ,
	firstDate- 6
,6 7
DateTime8 @

secondDateA K
,K L
IListM R
<R S
DateTimeS [
>[ \
publicHolidays] k
)k l
;l m
} 