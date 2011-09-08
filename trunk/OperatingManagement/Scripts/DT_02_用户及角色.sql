declare 
m_seqnum number(10);
begin
  m_seqnum:= to_number(fn_genseqnum('0002'));

  insert into TB_USER(USERID,
         LOGINNAME,
         DISPLAYNAME,
         PASSWORD1,
         USERTYPE,
         STATUS,
         MOBILE,
         NOTE,
         CTIME,
         LASTUPDATEDTIME) values(m_seqnum,
         'admin',
         '系统管理员',
         'XdZt2O+9K5JTQ1FOSGFeXp+B/7c=',
         1,
         0,
         null,
         null,
         sysdate(),
         sysdate());

end;
