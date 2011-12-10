


create or replace procedure UP_ComRes_SelectByID
(
       p_CRID TB_CommunicationResource.CRID%type,
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_CommunicationResource Where CRID=p_CRID;
end;


create or replace procedure UP_ComRes_SelectAll
(
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_CommunicationResource Order By CreatedTime Desc;
end;


create or replace procedure UP_ComRes_Insert
(
       p_RouteName TB_CommunicationResource.RouteName%type,
       p_RouteCode TB_CommunicationResource.RouteCode%type,
       p_Direction TB_CommunicationResource.Direction%type,
       p_BandWidth TB_CommunicationResource.BandWidth%type,
       p_Status TB_CommunicationResource.Status%type,
       p_ExtProperties TB_CommunicationResource.ExtProperties%type,
       p_CreatedTime TB_CommunicationResource.Createdtime%type,
       p_CreatedUserID TB_CommunicationResource.Createduserid%type,
       p_UpdatedTime TB_CommunicationResource.Updatedtime%type,
       p_UpdatedUserID TB_CommunicationResource.Updateduserid%type,
       v_CRID out TB_CommunicationResource.CRID%type,
       v_Result out number
)
is
begin  
       savepoint p1;
       v_CRID:=to_number(fn_genseqnum('4003'));
       Insert into TB_CommunicationResource(CRID,RouteName,RouteCode,Direction,BandWidth,Status,ExtProperties,CreatedTime,CreatedUserID,UpdatedTime,UpdatedUserID) 
       Values(v_CRID,p_RouteName,p_RouteCode,p_Direction,p_BandWidth,p_Status,p_ExtProperties,p_CreatedTime,p_CreatedUserID,p_UpdatedTime,p_UpdatedUserID);
       commit;
       v_Result:=5; -- Success
       
       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
       v_Result:=4; --Error
end;



create or replace procedure UP_ComRes_Update
(
       
       p_CRID TB_CommunicationResource.CRID%type,
       p_RouteName TB_CommunicationResource.RouteName%type,
       p_RouteCode TB_CommunicationResource.RouteCode%type,
       p_Direction TB_CommunicationResource.Direction%type,
       p_BandWidth TB_CommunicationResource.BandWidth%type,
       p_Status TB_CommunicationResource.Status%type,
       p_ExtProperties TB_CommunicationResource.ExtProperties%type,
       p_CreatedTime TB_CommunicationResource.Createdtime%type,
       p_CreatedUserID TB_CommunicationResource.Createduserid%type,
       p_UpdatedTime TB_CommunicationResource.Updatedtime%type,
       p_UpdatedUserID TB_CommunicationResource.Updateduserid%type,
       v_Result out number
)
is
begin
     savepoint p1;
     
       Update TB_CommunicationResource
       Set RouteName=p_RouteName
          ,RouteCode=p_RouteCode
          ,Direction=p_Direction
          ,BandWidth=p_BandWidth
          ,Status=p_Status
          ,ExtProperties=p_ExtProperties
          ,CreatedTime=p_CreatedTime
          ,CreatedUserID=p_CreatedUserID
          ,UpdatedTime=p_UpdatedTime
          ,UpdatedUserID=p_UpdatedUserID
        Where CRID=p_CRID;
        Commit;
        v_Result:=5; -- Success
       
        EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
        v_Result:=4; --Error
end;


