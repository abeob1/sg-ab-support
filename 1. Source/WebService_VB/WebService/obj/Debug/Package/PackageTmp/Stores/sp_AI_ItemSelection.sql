
/*
@Type: 1. Package items
	   0. Addon items
*/

create procedure [dbo].[sp_AI_ItemSelection]
@Type as bit
as
begin
	if @Type = 1
		select oi.Code, tm.ItemName, t1.Price as BasePrice, dbo.usf_getChildList(oi.Code) as Items from OITT oi 
			join OITM tm on oi.Code = tm.ItemCode
			join ITM1 t1 on t1.ItemCode = tm.ItemCode
		where t1.PriceList = 1
	else
		select tm.ItemCode, tm.ItemName, t1.Price as BasePrice, '' as Items from OITM tm	
			join ITM1 t1 on t1.ItemCode = tm.ItemCode
			 where tm.ItemCode not in (select tt.Code from OITT tt)
		and t1.PriceList = 1
	
end

-- exec sp_AI_ItemSelection 0

