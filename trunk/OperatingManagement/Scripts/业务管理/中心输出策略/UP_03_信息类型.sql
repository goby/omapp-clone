create or replace procedure UP_XXTYPE_SelectByID
(
       p_RID TB_XXTYPE.RID%type,
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_XXTYPE Where RID=p_RID;
end;


create or replace procedure UP_XXTYPE_SelectAll
(
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_XXTYPE Order By RID Desc;
end;