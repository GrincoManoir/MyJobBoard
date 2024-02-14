using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Enums
{
    public enum OpportunityState
    {
        // L'utilisateur à postulé et attend une proposition 
        APPLIED,
        // L'utilisateur passe des entretiens
        INTERVIEWING,
        // L'utilisateur est en attente d'une proposition suite aux entretiens passés
        WAITING_FOR_PROPOSITION,
        // Annulation du suivi de l'opportunité à l'initiative de l'utilisateur
        ABORTED,
        // La proposition a été refusée
        REFUSED,
        // La proposition a été acceptée
        VALIDATED

    }
}
