#if UNITY_ANDROID && ! UNITY_EDITOR
/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.11
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkThreadProperties : IDisposable {
  private global::System.IntPtr swigCPtr;
  protected bool swigCMemOwn;

  internal AkThreadProperties(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  internal static global::System.IntPtr getCPtr(AkThreadProperties obj) {
    return (obj == null) ? global::System.IntPtr.Zero : obj.swigCPtr;
  }

  internal virtual void setCPtr(global::System.IntPtr cPtr) {
    Dispose();
    swigCPtr = cPtr;
  }

  ~AkThreadProperties() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkThreadProperties(swigCPtr);
        }
        swigCPtr = global::System.IntPtr.Zero;
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public int nPriority { set { AkSoundEnginePINVOKE.CSharp_AkThreadProperties_nPriority_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkThreadProperties_nPriority_get(swigCPtr); } 
  }

  public uint uStackSize { set { AkSoundEnginePINVOKE.CSharp_AkThreadProperties_uStackSize_set(swigCPtr, value); } 
    get {
      uint ret = AkSoundEnginePINVOKE.CSharp_AkThreadProperties_uStackSize_get(swigCPtr);

      return ret;
    } 
  }

  public int uSchedPolicy { set { AkSoundEnginePINVOKE.CSharp_AkThreadProperties_uSchedPolicy_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkThreadProperties_uSchedPolicy_get(swigCPtr); } 
  }

  public uint dwAffinityMask { set { AkSoundEnginePINVOKE.CSharp_AkThreadProperties_dwAffinityMask_set(swigCPtr, value); }  get { return AkSoundEnginePINVOKE.CSharp_AkThreadProperties_dwAffinityMask_get(swigCPtr); } 
  }

  public AkThreadProperties() : this(AkSoundEnginePINVOKE.CSharp_new_AkThreadProperties(), true) {
  }

}
#endif // #if UNITY_ANDROID && ! UNITY_EDITOR