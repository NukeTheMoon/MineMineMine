using UnityEngine;
using System.Collections;

public static class ProportionHelper
{

    // rI  -  rO
    //     X
    // gI  -  ret
    // 
    // rO * gI / rI = ret

    public static float LinearOutput(float referenceInput, float referenceOutput, float givenInput)
    {
        return givenInput * referenceOutput / referenceInput;
    }

    // rI  -  rO
    //     X
    // ret -  gO
    //
    // rI * gO / rO = ret

    public static float LinearInput(float referenceInput, float referenceOutput, float givenOutput)
    {
        return referenceInput * givenOutput / referenceOutput;
    }

}
