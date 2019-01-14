 select taxvalue from TaxesConfig tc
 inner join Taxes t on tc.Id = t.TaxID
 inner join Municipality m on m.id = t.MunID
 where m.Municipality = 'Vilnius'
 and t.DateFrom = '2016-01-01'
 order by TaxTypeNo 

 select *  from update taxes set DateTo = '2016-01-01'
 where id = 1
 insert into Taxes
 values(1,2,'2016-05-01','2018-12-12')
  select * from TaxesConfig
  select * from Municipality