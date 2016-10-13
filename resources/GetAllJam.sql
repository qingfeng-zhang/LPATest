SELECT * from TblInOut 
WHERE ( (stat=22 or stat=33 or stat=44 or stat=55 or stat=66 or stat=77 or stat=88) 
or (stat=222 or stat=333 or stat=445 or stat=555 or stat=666 or stat=777 or stat=888)  )
AND  ( FL <> 'FFFFFFFFFF' )
Order by DTOut desc
