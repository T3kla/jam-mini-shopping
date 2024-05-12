using System.Collections.Generic;
using Data;
using UnityEngine;

public class Belt : MonoBehaviour
{
    public Board board = null;
    
    public FoobCard foobCardPrefab = null;
    public float spawnRate = 2f;
    
    private readonly List<FoobCard> _cards = new ();
    private bool _isDealing = false;
    
    public void StartDealingCards(List<FoobData> foobs, float beltSpeed)
    {
        if (_isDealing) return;
        _isDealing = true;
        
        Loop(foobs, beltSpeed);
    }
    
    public void StopDealingCards()
    {
        _isDealing = false;
        
        foreach (var card in _cards)
            Destroy(card.gameObject);
        
        _cards.Clear();
    }

    private async void Loop(IReadOnlyList<FoobData> foobs, float beltSpeed)
    {
        while (true)
        {
            if (!_isDealing) break;
            
            await Awaitable.WaitForSecondsAsync(1 / spawnRate / beltSpeed);
            
            var foob = foobs[Random.Range(0, foobs.Count)];
            var card = Instantiate(foobCardPrefab, transform.position, Quaternion.identity);
            var rot = Random.Range(0, 3);
            card.Init(foob, rot, beltSpeed);
            _cards.Add(card);
        }
    }
}