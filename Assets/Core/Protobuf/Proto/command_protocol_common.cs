//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: command_protocol_common.proto
namespace PROTOCOL_COMMON
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserInfo")]
  public partial class UserInfo : global::ProtoBuf.IExtensible
  {
    public UserInfo() {}
    
    private string _user_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"user_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string user_name
    {
      get { return _user_name; }
      set { _user_name = value; }
    }
    private int _user_seat = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"user_seat", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int user_seat
    {
      get { return _user_seat; }
      set { _user_seat = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RoomInfo")]
  public partial class RoomInfo : global::ProtoBuf.IExtensible
  {
    public RoomInfo() {}
    
    private string _room_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"room_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string room_name
    {
      get { return _room_name; }
      set { _room_name = value; }
    }
    private readonly global::System.Collections.Generic.List<PROTOCOL_COMMON.UserInfo> _user_list = new global::System.Collections.Generic.List<PROTOCOL_COMMON.UserInfo>();
    [global::ProtoBuf.ProtoMember(2, Name=@"user_list", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<PROTOCOL_COMMON.UserInfo> user_list
    {
      get { return _user_list; }
    }
  
    private int _room_unique_id = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"room_unique_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int room_unique_id
    {
      get { return _room_unique_id; }
      set { _room_unique_id = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"pre_battle_data")]
  public partial class pre_battle_data : global::ProtoBuf.IExtensible
  {
    public pre_battle_data() {}
    
    private string _level_name = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"level_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string level_name
    {
      get { return _level_name; }
      set { _level_name = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}