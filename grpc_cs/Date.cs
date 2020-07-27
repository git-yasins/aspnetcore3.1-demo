// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: date.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from date.proto</summary>
public static partial class DateReflection {

  #region Descriptor
  /// <summary>File descriptor for date.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static DateReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgpkYXRlLnByb3RvIjAKBERhdGUSDAoEeWVhchgBIAEoBRINCgVtb250aBgC",
          "IAEoBRILCgNkYXkYAyABKAViBnByb3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::Date), global::Date.Parser, new[]{ "Year", "Month", "Day" }, null, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class Date : pb::IMessage<Date>
#if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    , pb::IBufferMessage
#endif
{
  private static readonly pb::MessageParser<Date> _parser = new pb::MessageParser<Date>(() => new Date());
  private pb::UnknownFieldSet _unknownFields;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<Date> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::DateReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Date() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Date(Date other) : this() {
    year_ = other.year_;
    month_ = other.month_;
    day_ = other.day_;
    _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public Date Clone() {
    return new Date(this);
  }

  /// <summary>Field number for the "year" field.</summary>
  public const int YearFieldNumber = 1;
  private int year_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Year {
    get { return year_; }
    set {
      year_ = value;
    }
  }

  /// <summary>Field number for the "month" field.</summary>
  public const int MonthFieldNumber = 2;
  private int month_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Month {
    get { return month_; }
    set {
      month_ = value;
    }
  }

  /// <summary>Field number for the "day" field.</summary>
  public const int DayFieldNumber = 3;
  private int day_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int Day {
    get { return day_; }
    set {
      day_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as Date);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(Date other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (Year != other.Year) return false;
    if (Month != other.Month) return false;
    if (Day != other.Day) return false;
    return Equals(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (Year != 0) hash ^= Year.GetHashCode();
    if (Month != 0) hash ^= Month.GetHashCode();
    if (Day != 0) hash ^= Day.GetHashCode();
    if (_unknownFields != null) {
      hash ^= _unknownFields.GetHashCode();
    }
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (Year != 0) {
      output.WriteRawTag(8);
      output.WriteInt32(Year);
    }
    if (Month != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(Month);
    }
    if (Day != 0) {
      output.WriteRawTag(24);
      output.WriteInt32(Day);
    }
    if (_unknownFields != null) {
      _unknownFields.WriteTo(output);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (Year != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Year);
    }
    if (Month != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Month);
    }
    if (Day != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(Day);
    }
    if (_unknownFields != null) {
      size += _unknownFields.CalculateSize();
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(Date other) {
    if (other == null) {
      return;
    }
    if (other.Year != 0) {
      Year = other.Year;
    }
    if (other.Month != 0) {
      Month = other.Month;
    }
    if (other.Day != 0) {
      Day = other.Day;
    }
    _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    input.ReadRawMessage(this);
  #else
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
          break;
        case 8: {
          Year = input.ReadInt32();
          break;
        }
        case 16: {
          Month = input.ReadInt32();
          break;
        }
        case 24: {
          Day = input.ReadInt32();
          break;
        }
      }
    }
  #endif
  }

  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
          break;
        case 8: {
          Year = input.ReadInt32();
          break;
        }
        case 16: {
          Month = input.ReadInt32();
          break;
        }
        case 24: {
          Day = input.ReadInt32();
          break;
        }
      }
    }
  }
  #endif

}

#endregion


#endregion Designer generated code
