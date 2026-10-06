using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class XRInputDeviceButtonRecorded : MonoBehaviour, IXRInputButtonReader
{
    public float SimulatedButtonValue { get; set; } = 0;

    bool m_IsPerformed;
    bool m_WasPerformedThisFrame;
    bool m_WasCompletedThisFrame;

    /// <summary>
    /// See <see cref="MonoBehaviour"/>.
    /// </summary>
    void Update()
    {
        var prevPerformed = m_IsPerformed;
        m_IsPerformed = getBoolValue();
        m_WasPerformedThisFrame = !prevPerformed && m_IsPerformed;
        m_WasCompletedThisFrame = prevPerformed && !m_IsPerformed;
    }

    /// <inheritdoc />
    public bool ReadIsPerformed()
    {
        return m_IsPerformed;
    }

    /// <inheritdoc />
    public bool ReadWasPerformedThisFrame()
    {
        return m_WasPerformedThisFrame;
    }

    /// <inheritdoc />
    public bool ReadWasCompletedThisFrame()
    {
        return m_WasCompletedThisFrame;
    }

    /// <inheritdoc />
    public float ReadValue()
    {
        return SimulatedButtonValue;
    }

    /// <inheritdoc />
    public bool TryReadValue(out float value)
    {
        value = SimulatedButtonValue;
        return false;
    }

    private bool getBoolValue()
    {
        return Math.Round(SimulatedButtonValue, 2) != 0;
    }
}

