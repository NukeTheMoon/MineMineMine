using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AsteroidDivider : MonoBehaviour {

    public GameObject AsteroidPrefab;
    public int Divisor = 2;
    [Range(0, 6)]
    public int Divisions = 6;
    public float PositionJitter = 1;
    public float Force = 100;

    public static Dictionary<int, int> DivisionReference = new Dictionary<int, int>();

    private int _id;
    private int _firstDivision = 1;
    private float _scaleStep = 0.1f;
    private int _maxDivisions = 6;
    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _id = gameObject.GetInstanceID();
        _rigidbody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == TagsReference.MISSILE)
        {
            if (CanDivideAsteroid())
            {
                Divide();
            }
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void Divide()
    {
        for (var i = 0; i < Divisor; ++i)
        {
            var divisionNumber = _firstDivision;
            if (DivisionReference.ContainsKey(_id))
            {
                divisionNumber = Math.Min(_maxDivisions, DivisionReference[_id] + 1);
            }
            var smallerAsteroid = (GameObject)Instantiate(AsteroidPrefab, JitterPosition(transform.position, divisionNumber), Quaternion.identity); // why (clone)(clone)??
            DivisionReference.Add(smallerAsteroid.GetInstanceID(), divisionNumber);
            smallerAsteroid.name = "Asteroid:" + divisionNumber;
            ApplySize(smallerAsteroid, divisionNumber);
            PropelAwayFromOriginal(smallerAsteroid);

        }
    }

    private void PropelAwayFromOriginal(GameObject smallerAsteroid)
    {
        var direction = smallerAsteroid.transform.position - transform.position;
        var rigidbody = smallerAsteroid.GetComponent<Rigidbody>();
        rigidbody.AddForce(direction * Force);
        rigidbody.drag = 0;
    }

    private Vector3 JitterPosition(Vector3 position, int divisionNumber)
    {
        var scaleModifier = GetScale(divisionNumber);
        var x = position.x + (UnityEngine.Random.Range(-PositionJitter, PositionJitter) * scaleModifier);
        var z = position.z + (UnityEngine.Random.Range(-PositionJitter, PositionJitter) * scaleModifier);
        return new Vector3(x, position.y, z);
    }

    private bool CanDivideAsteroid()
    {
        return (!(DivisionReference.ContainsKey(_id) && DivisionReference[_id] >= Divisions) && Divisions > 0);
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
