SELECT 
	usr."Id",
	usr."ExternalId",
	usr."Name",
	usr."TotalBalance",
	expense."Id",
	expense."Description" ,
	expense."Amount" ,
	expense."Category" ,
	income."Id",
	income."Description" ,
	income."Amount" ,
	income."IncomeType",
	goal."Id",
	goal."Title" ,
	goal."Description" ,
	goal."Amount" ,
	goal."DeductedRatio",
	goal."Progress"
FROM
	"Users" usr
LEFT JOIN
	"Expenses" expense
	ON expense."UserId"=usr."Id"
LEFT JOIN
	"Incomes" income
	ON income."UserId"=usr."Id"
LEFT JOIN
	"Goals" goal
	ON goal."UserId"=usr."Id"
WHERE 
	usr."ExternalId"=@externalId