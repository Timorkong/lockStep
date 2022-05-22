//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: command_protocol_room.proto
// Note: requires additional types generated from: command_protocol_common.proto
namespace PROTOCOL_ROOM
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_ROOM_LIST_REQ")]
  public partial class CMD_ROOM_LIST_REQ : global::ProtoBuf.IExtensible
  {
    public CMD_ROOM_LIST_REQ() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_ROOM_LIST_RSP")]
  public partial class CMD_ROOM_LIST_RSP : global::ProtoBuf.IExtensible
  {
    public CMD_ROOM_LIST_RSP() {}
    
    private readonly global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo> _room_list = new global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"room_list", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo> room_list
    {
      get { return _room_list; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_CREATE_ROOM_REQ")]
  public partial class CMD_CREATE_ROOM_REQ : global::ProtoBuf.IExtensible
  {
    public CMD_CREATE_ROOM_REQ() {}
    
    private string _room_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"room_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string room_name
    {
      get { return _room_name; }
      set { _room_name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_CREATE_ROOM_RSP")]
  public partial class CMD_CREATE_ROOM_RSP : global::ProtoBuf.IExtensible
  {
    public CMD_CREATE_ROOM_RSP() {}
    
    private int _player_seat = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"player_seat", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int player_seat
    {
      get { return _player_seat; }
      set { _player_seat = value; }
    }
    private PROTOCOL_COMMON.RoomInfo _room_info = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"room_info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public PROTOCOL_COMMON.RoomInfo room_info
    {
      get { return _room_info; }
      set { _room_info = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_LEAVE_ROOM_REQ")]
  public partial class CMD_LEAVE_ROOM_REQ : global::ProtoBuf.IExtensible
  {
    public CMD_LEAVE_ROOM_REQ() {}
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_LEAVE_ROOM_RSP")]
  public partial class CMD_LEAVE_ROOM_RSP : global::ProtoBuf.IExtensible
  {
    public CMD_LEAVE_ROOM_RSP() {}
    
    private readonly global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo> _room_list = new global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo>();
    [global::ProtoBuf.ProtoMember(1, Name=@"room_list", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PROTOCOL_COMMON.RoomInfo> room_list
    {
      get { return _room_list; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_UPDATE_ROOM_INFO_NOTICE")]
  public partial class CMD_UPDATE_ROOM_INFO_NOTICE : global::ProtoBuf.IExtensible
  {
    public CMD_UPDATE_ROOM_INFO_NOTICE() {}
    
    private PROTOCOL_COMMON.RoomInfo _room_info = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"room_info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public PROTOCOL_COMMON.RoomInfo room_info
    {
      get { return _room_info; }
      set { _room_info = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_JOIN_ROOM_REQ")]
  public partial class CMD_JOIN_ROOM_REQ : global::ProtoBuf.IExtensible
  {
    public CMD_JOIN_ROOM_REQ() {}
    
    private int _room_unquie_id = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"room_unquie_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int room_unquie_id
    {
      get { return _room_unquie_id; }
      set { _room_unquie_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_JOIN_ROOM_RSP")]
  public partial class CMD_JOIN_ROOM_RSP : global::ProtoBuf.IExtensible
  {
    public CMD_JOIN_ROOM_RSP() {}
    
    private int _player_seat = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"player_seat", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int player_seat
    {
      get { return _player_seat; }
      set { _player_seat = value; }
    }
    private PROTOCOL_COMMON.RoomInfo _room_info = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"room_info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public PROTOCOL_COMMON.RoomInfo room_info
    {
      get { return _room_info; }
      set { _room_info = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}