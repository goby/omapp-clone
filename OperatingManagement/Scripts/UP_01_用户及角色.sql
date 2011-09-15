create or replace procedure up_user_selectbyloginname
(
       p_LoginName tb_user.loginname%type,
       o_cursor out sys_refcursor
)
is
begin
       open o_cursor for
            select * from tb_user where LoginName=p_LoginName;
end;

create or replace procedure up_user_selectall
(
       o_cursor out sys_refcursor  
)
is 
begin
       open o_cursor for
            select * from tb_user;
end;
  
create or replace procedure up_role_selectall
(
       o_cursor out sys_refcursor  
)
is 
begin
       open o_cursor for
            select * from tb_role;
end;
