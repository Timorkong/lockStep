# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: common_table.proto
"""Generated protocol buffer code."""
from google.protobuf.internal import enum_type_wrapper
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='common_table.proto',
  package='Table',
  syntax='proto2',
  serialized_options=None,
  create_key=_descriptor._internal_create_key,
  serialized_pb=b'\n\x12\x63ommon_table.proto\x12\x05Table\";\n\x04\x41\x41\x42\x42\x12\t\n\x01x\x18\x01 \x01(\x02\x12\t\n\x01y\x18\x02 \x01(\x02\x12\r\n\x05width\x18\x03 \x01(\x02\x12\x0e\n\x06height\x18\x04 \x01(\x02*Q\n\x08\x42odyType\x12\x12\n\x0e\x42ODY_TYPE_NONE\x10\x00\x12\x0f\n\x0b\x42ODY_TYPE_M\x10\x01\x12\x0f\n\x0b\x42ODY_TYPE_F\x10\x02\x12\x0f\n\x0b\x42ODY_TYPE_C\x10\x03'
)

_BODYTYPE = _descriptor.EnumDescriptor(
  name='BodyType',
  full_name='Table.BodyType',
  filename=None,
  file=DESCRIPTOR,
  create_key=_descriptor._internal_create_key,
  values=[
    _descriptor.EnumValueDescriptor(
      name='BODY_TYPE_NONE', index=0, number=0,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='BODY_TYPE_M', index=1, number=1,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='BODY_TYPE_F', index=2, number=2,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
    _descriptor.EnumValueDescriptor(
      name='BODY_TYPE_C', index=3, number=3,
      serialized_options=None,
      type=None,
      create_key=_descriptor._internal_create_key),
  ],
  containing_type=None,
  serialized_options=None,
  serialized_start=90,
  serialized_end=171,
)
_sym_db.RegisterEnumDescriptor(_BODYTYPE)

BodyType = enum_type_wrapper.EnumTypeWrapper(_BODYTYPE)
BODY_TYPE_NONE = 0
BODY_TYPE_M = 1
BODY_TYPE_F = 2
BODY_TYPE_C = 3



_AABB = _descriptor.Descriptor(
  name='AABB',
  full_name='Table.AABB',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  create_key=_descriptor._internal_create_key,
  fields=[
    _descriptor.FieldDescriptor(
      name='x', full_name='Table.AABB.x', index=0,
      number=1, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='y', full_name='Table.AABB.y', index=1,
      number=2, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='width', full_name='Table.AABB.width', index=2,
      number=3, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
    _descriptor.FieldDescriptor(
      name='height', full_name='Table.AABB.height', index=3,
      number=4, type=2, cpp_type=6, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR,  create_key=_descriptor._internal_create_key),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto2',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=29,
  serialized_end=88,
)

DESCRIPTOR.message_types_by_name['AABB'] = _AABB
DESCRIPTOR.enum_types_by_name['BodyType'] = _BODYTYPE
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

AABB = _reflection.GeneratedProtocolMessageType('AABB', (_message.Message,), {
  'DESCRIPTOR' : _AABB,
  '__module__' : 'common_table_pb2'
  # @@protoc_insertion_point(class_scope:Table.AABB)
  })
_sym_db.RegisterMessage(AABB)


# @@protoc_insertion_point(module_scope)
