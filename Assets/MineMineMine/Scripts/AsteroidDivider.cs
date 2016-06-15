using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AsteroidDivider : MonoBehaviour {


    private int _id;
    private int _firstDivision = 1;
    private float _scaleStep = 0.1f;
    private int _maxDivisions = 6;
    private Rigidbody _rigidbody;

    private void Start () {
        _id = gameObject.GetInstanceID();
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.MISSILE)
        {
            if (CanDivideAsteroid())
            {
                Divide();
            }
            Destroy(gameObject);
            Punchthrough(other.gameObject);
            Instantiate(PrefabReference.Yield, transform.position, Quaternion.identity);
        }
    }

    private void Punchthrough(GameObject missile)
    {
        if (!SceneReference.WeaponManager.IsPunchthrough(missile.GetInstanceID()))
        {
            Destroy(missile);
        }
    }

    private void Divide()
    {
        for (var i = 0; i < SceneReference.AsteroidDivisionManager.Divisor; ++i)
        {
            var divisionNumber = _firstDivision;
            if (SceneReference.AsteroidDivisionManager.DivisionReference.ContainsKey(_id))
            {
                divisionNumber = Math.Min(_maxDivisions, SceneReference.AsteroidDivisionManager.DivisionReference[_id] + 1);
            }
            var smallerAsteroid = (GameObject)Instantiate(PrefabReference.Asteroid, JitterPosition(transform.position, divisionNumber), Quaternion.identity);
            SceneReference.AsteroidDivisionManager.DivisionReference.Add(smallerAsteroid.GetInstanceID(), divisionNumber);
            smallerAsteroid.name = "Asteroid:" + divisionNumber;
            ApplySize(smallerAsteroid, divisionNumber);
            PropelAwayFromOriginal(smallerAsteroid);

        }
    }

    private void PropelAwayFromOriginal(GameObject smallerAsteroid)
    {
        var direction = smallerAsteroid.transform.position - transform.position;
        var rigidbody = smallerAsteroid.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * SceneReference.AsteroidDivisionManager.Force);
        rigidbody.drag = 0;
    }

    private Vector3 JitterPosition(Vector3 position, int divisionNumber)
    {
        var scaleModifier = GetScale(divisionNumber);
        var x = position.x + (UnityEngine.Random.Range(-SceneReference.AsteroidDivisionManager.PositionJitter, SceneReference.AsteroidDivisionManager.PositionJitter) * scaleModifier);
        var z = position.z + (UnityEngine.Random.Range(-SceneReference.AsteroidDivisionManager.PositionJitter, SceneReference.AsteroidDivisionManager.PositionJitter) * scaleModifier);
        return new Vector3(x, position.y, z);
    }

    private bool CanDivideAsteroid()
    {
        return (!(SceneReference.AsteroidDivisionManager.DivisionReference.ContainsKey(_id) &&
            SceneReference.AsteroidDivisionManager.DivisionReference[_id] >= SceneReference.AsteroidDivisionManager.Divisions) && 
            SceneReference.AsteroidDivisionManager.Divisions > 0);
    }

    private void ApplySize(GameObject asteroid, int divisionNumber)
    {
        var scale = GetScale(divisionNumber);
        asteroid.transform.localScale = new Vector3(scale, scale, scale);
    }

    private float GetScale(int divisionNumber)
    {
        return 1.0f - (_scaleStep * divisionNumber);
    }
}
