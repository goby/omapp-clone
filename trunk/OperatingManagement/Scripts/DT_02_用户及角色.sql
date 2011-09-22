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
         'ϵͳ����Ա',
         'XdZt2O+9K5JTQ1FOSGFeXp+B/7c=',
         1,
         0,
         null,
         null,
         sysdate(),
         sysdate());
  commit;
end;
begin
  insert into tb_action values(1,'Add','����',sysdate());
  insert into tb_action values(2,'Edit','�޸�',sysdate());
  insert into tb_action values(3,'Delete','ɾ��',sysdate());
  insert into tb_action values(4,'View','�鿴',sysdate());
  insert into tb_action values(5,'List','�б�',sysdate());
  commit;
end;
begin
  insert into tb_module values(1,'UserManage','�û�����',sysdate());
  insert into tb_module values(2,'RoleManage','��ɫ����',sysdate());
  commit;
end;
begin
  --UserManage
  insert into tb_permission values(1,1,1);
  insert into tb_permission values(2,1,2);
  insert into tb_permission values(3,1,3);
  insert into tb_permission values(4,1,4);
  insert into tb_permission values(5,1,5);  
  --RoleManage
  insert into tb_permission values(6,2,1);
  insert into tb_permission values(7,2,2);
  insert into tb_permission values(8,2,3);
  insert into tb_permission values(9,2,4);
  insert into tb_permission values(10,2,5);
  commit;
end;
