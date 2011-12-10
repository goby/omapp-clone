


create or replace procedure UP_GroundRes_SelectByID
(
       p_GRID TB_GROUNDRESOURCE.GRID%type,
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_GroundResource Where GRID=p_GRID;
end;


create or replace procedure UP_GroundRes_SelectAll
(
       o_Cursor out sys_refcursor
)
is
begin
       open o_Cursor for
            Select * From TB_GroundResource Order By CreatedTime Desc;
end;


create or replace procedure UP_GroundRes_Insert
(
       p_GRName TB_GroundResource.GRName%type,
       p_GRCode TB_GroundResource.GRCode%type,
       p_EquipmentName TB_GroundResource.EquipmentName%type,
       p_EquipmentCode TB_GroundResource.EquipmentCode%type,
       p_Owner TB_GroundResource.Owner%type,
       p_Coordinate TB_GroundResource.Coordinate%type,
       p_FunctionType TB_GroundResource.FunctionType%type,
       p_Status TB_GroundResource.Status%type,
       p_ExtProperties TB_GroundResource.ExtProperties%type,
       p_CreatedTime TB_GroundResource.Createdtime%type,
       p_CreatedUserID TB_GroundResource.Createduserid%type,
       p_UpdatedTime TB_GroundResource.Updatedtime%type,
       p_UpdatedUserID TB_GroundResource.Updateduserid%type,
       v_GRID out TB_GroundResource.GRID%type,
       v_Result out number
)
is
begin  
       savepoint p1;
       v_GRID:=to_number(fn_genseqnum('4002'));
       Insert into TB_GroundResource(GRID,GRCode,EquipmentName,EquipmentCode,Owner,Coordinate,FunctionType,Status,ExtProperties,CreatedTime,CreatedUserID,UpdatedTime,UpdatedUserID) 
       Values(v_GRID,p_GRCode,p_EquipmentName,p_EquipmentCode,p_Owner,p_Coordinate,p_FunctionType,p_Status,p_ExtProperties,p_CreatedTime,p_CreatedUserID,p_UpdatedTime,p_UpdatedUserID);
       commit;
       v_Result:=5; -- Success
       
       EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
       v_Result:=4; --Error
end;



create or replace procedure UP_GroundRes_Update
(
       p_GRID out TB_GroundResource.GRID%type,
       p_GRName TB_GroundResource.GRName%type,
       p_GRCode TB_GroundResource.GRCode%type,
       p_EquipmentName TB_GroundResource.EquipmentName%type,
       p_EquipmentCode TB_GroundResource.EquipmentCode%type,
       p_Owner TB_GroundResource.Owner%type,
       p_Coordinate TB_GroundResource.Coordinate%type,
       p_FunctionType TB_GroundResource.FunctionType%type,
       p_Status TB_GroundResource.Status%type,
       p_ExtProperties TB_GroundResource.ExtProperties%type,
       p_CreatedTime TB_GroundResource.Createdtime%type,
       p_CreatedUserID TB_GroundResource.Createduserid%type,
       p_UpdatedTime TB_GroundResource.Updatedtime%type,
       p_UpdatedUserID TB_GroundResource.Updateduserid%type,
       v_Result out number
)
is
begin
     savepoint p1;
     
     Update TB_GroundResource
     Set GRCode=p_GRCode
        ,EquipmentName=p_EquipmentName
        ,EquipmentCode=p_EquipmentCode
        ,Owner=p_Owner
        ,Coordinate=p_Coordinate
        ,FunctionType=p_FunctionType
        ,Status=p_Status
        ,ExtProperties=p_ExtProperties
        ,CreatedTime=p_CreatedTime
        ,CreatedUserID=p_CreatedUserID
        ,UpdatedTime=p_UpdatedTime
        ,UpdatedUserID=p_UpdatedUserID
        where GRID=p_GRID;
        commit;
        v_Result:=5; -- Success
       
        EXCEPTION
        WHEN OTHERS THEN
          ROLLBACK TO SAVEPOINT p1;
          COMMIT;
        v_Result:=4; --Error
end;


