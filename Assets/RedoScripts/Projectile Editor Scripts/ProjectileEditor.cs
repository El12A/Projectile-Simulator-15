using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace PhysicsProjectileSimulator
{
    public class ProjectileEditor : SceneController
    {
        [SerializeField] private TMP_Dropdown shapeDropdown;
        [SerializeField] private TMP_Dropdown materialDropdown;
        [SerializeField] private TMP_InputField radiusInput;
        [SerializeField] private TMP_InputField lengthInput;
        [SerializeField] private TMP_InputField widthInput;
        [SerializeField] private TMP_InputField heightInput;
        [SerializeField] private Slider restitutionSlider;
        [SerializeField] private Slider frictionCoefficientSlider;
        [SerializeField] private TMP_Text densityText;
        [SerializeField] private TMP_Text massText;
        [SerializeField] private TMP_Text volumeText;
        [SerializeField] private Material basicMaterial;

        [SerializeField] private GameObject RadiusObject;
        [SerializeField] private GameObject LengthObject;
        [SerializeField] private GameObject HeightObject;
        [SerializeField] private GameObject WidthObject;

        [SerializeField] private InputFieldRestrictor inputFieldRestrictor;

        private string material;
        private float density;
        private float volume;
        private float mass;
        private float radius;
        private float length;
        private float height;
        private float width;
        private float restitution;
        private float frictionCoefficient;

        private string selectedString;
        private GameObject projectile1;


        private void Start()
        {
            selectedString = "Sphere";
            material = "Wood";
            radius = 0.1f;
            length = 0.2f;
            height = 0.2f;
            width = 0.2f;
            frictionCoefficient = 0.5f;
            restitution = 0.5f;
            projectile1 = GameObject.FindWithTag("projectile");
            projectile = projectile1.GetComponent<Projectile>();
            projectile.materialName = material;
        }

        private void UpdateProjectile()
        {
            int selectedIndex = shapeDropdown.value;
            density = projectile.GetDensity();
            if (selectedString == "Sphere")
            {
                volume = projectile.sphereProjectile.CalculateVolume();
                mass = projectile.sphereProjectile.CalculateMass();
                projectile.sphereProjectile.SetMesh();
                projectile.sphereProjectile.SetScale();
                projectile.sphereProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cube")
            {
                volume = projectile.cubeProjectile.CalculateVolume();
                mass = projectile.cubeProjectile.CalculateMass();
                projectile.cubeProjectile.SetMesh();
                projectile.cubeProjectile.SetScale();
                projectile.cubeProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cylinder")
            {
                volume = projectile.cylinderProjectile.CalculateVolume();
                mass = projectile.cylinderProjectile.CalculateMass();
                projectile.cylinderProjectile.SetMesh();
                projectile.cylinderProjectile.SetScale();
                projectile.cylinderProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Cone")
            {
                volume = projectile.coneProjectile.CalculateVolume();
                mass = projectile.coneProjectile.CalculateMass();
                projectile.coneProjectile.SetMesh();
                projectile.coneProjectile.SetScale();
                projectile.coneProjectile.UpdateRigidbody();
            }
            else if (selectedString == "Teardrop")
            {
                volume = projectile.teardropProjectile.CalculateVolume();
                mass = projectile.teardropProjectile.CalculateMass();
                projectile.teardropProjectile.SetMesh();
                projectile.teardropProjectile.SetScale();
                projectile.teardropProjectile.UpdateRigidbody();
            }
            projectile.UpdateMeshesCollidersAndPhyiscsMaterial();
            massText.text = "Mass: " + ReturnRoudedString(mass, 3) + "kg";
            volumeText.text = "Volume: " + ReturnRoudedString(volume, 3) + "m^3";
        }


        public void OnShapeDropdownChange()
        {
            selectedString = shapeDropdown.options[shapeDropdown.value].text;
            if (selectedString == "Sphere")
            {
                SetMeasurementsActive(true, false, false, false);
                projectile.projectileShape = "Sphere";
            }
            else if (selectedString == "Cube")
            {
                SetMeasurementsActive(false, true, true, true);
                projectile.projectileShape = "Cube";
            }
            else if (selectedString == "Cylinder")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Cylinder";
            }
            else if (selectedString == "Cone")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Cone";
            }
            else if (selectedString == "Teardrop")
            {
                SetMeasurementsActive(true, false, false, true);
                projectile.projectileShape = "Teardrop";
            }
            UpdateProjectile();
        }

        public void OnMaterialDropdownChanger()
        {
            int selectedIndex = materialDropdown.value;
            material = materialDropdown.options[selectedIndex].text;
            projectile.materialName = material;
            // calling this function updates the projectile class and its variables
            UpdateProjectile();
            densityText.text = "Density: " + density.ToString() + "Kg/m^3";
        }

        public void OnAnyMeasurementInputValueChanged(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnValueChanged();
        }
        public void OnAnyMeasurementInputEndEdit(TMP_InputField input)
        {
            inputFieldRestrictor.UpdateInputField(input);
            inputFieldRestrictor.OnValueChanged();
        }

        public void SetRadius()
        {
            radius = float.Parse(radiusInput.text);
            projectile.sphereProjectile.radius = radius;
            projectile.cylinderProjectile.radius = radius;
            projectile.coneProjectile.radius = radius;
            UpdateProjectile();
            Debug.Log(projectile1.transform.transform.localScale);
        }
        public void SetLength()
        {
            length = float.Parse(lengthInput.text);
            projectile.cubeProjectile.length = length;
            UpdateProjectile();
        }
        public void SetWidth()
        {
            width = float.Parse(widthInput.text);
            projectile.cubeProjectile.width = width;
            UpdateProjectile();
        }
        public void SetHeight()
        {
            height = float.Parse(heightInput.text);
            projectile.sphereProjectile.height = height;
            projectile.cubeProjectile.height = height;
            projectile.cylinderProjectile.height = height;
            projectile.coneProjectile.height = height;
            projectile.teardropProjectile.height = height;
            UpdateProjectile();
        }

        public void OnRestitutionSliderChange()
        {
            restitution = restitutionSlider.value;
            projectile.restitution = restitution;
            UpdateProjectile();
        }

        public void OnCoefficientOfFrictionSliderChange()
        {
            frictionCoefficient = frictionCoefficientSlider.value;
            projectile.frictionalCoefficient = frictionCoefficient;
            UpdateProjectile();
        }

        private void SetMeasurementsActive(bool M1, bool M2, bool M3, bool M4)
        {
            RadiusObject.SetActive(M1);
            LengthObject.SetActive(M2);
            WidthObject.SetActive(M3);
            HeightObject.SetActive(M4);
        }

        private string ReturnRoudedString(float num, int sigFigs)
        {
            string numStr = num.ToString();
            if (numStr.Contains('.'))
            {
                // means the right number of desired sf is  reached even when a decimal point is used in the input
                sigFigs++;
            }
            return numStr.Substring(0, Mathf.Min(numStr.Length, sigFigs));
        }
    }
}

