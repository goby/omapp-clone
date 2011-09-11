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

begin
exec up_user_selectbyloginname('admin');
end
