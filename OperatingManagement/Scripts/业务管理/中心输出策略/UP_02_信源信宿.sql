create or replace procedure UP_XYXSINFO_SelectByID
(
       p_RID TB_XYXSINFO.RID%type,
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_XYXSINFO Where RID=p_RID;
end;


create or replace procedure UP_XYXSINFO_SelectAll
(
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_XYXSINFO Order By ADDRName;
end;