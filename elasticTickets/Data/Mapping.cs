using elasticTickets.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace elasticTickets.Data
{
    public static class Mapping
    {
        public static CreateIndexDescriptor ActorMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Actor>(m => m.Properties(p => p
                //.Number(t => t.Name(n => n.id).Type(NumberType.Integer))
                .Text(t => t.Name(n => n.fullName))
                .Text(t => t.Name(n => n.profilePictureURL))
                .Text(t => t.Name(n => n.bio))
            )
            );
        }
        public static CreateIndexDescriptor MoviesMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Movie>(m => m.Properties(p => p
                //.Number(t => t.Name(n => n.id).Type(NumberType.Integer))
                .Text(t => t.Name(n => n.name))
                .Text(t => t.Name(n => n.description))
                .Date(t => t.Name(n => n.startDate))
                .Date(t => t.Name(n => n.endDate))
                .Text(t => t.Name(n => n.imageURL))
                .Text(t => t.Name(n => n.movieCinema))
                .Text(t => t.Name(n => n.movieProducer))
                .Text(t => t.Name(n => n.movieCategory))
                .Text(t => t.Name(n => n.price))
                .Object<Actor>(t => t.Name(n => n.movieActorsIds))
            )
            );
        }
        public static CreateIndexDescriptor CinemaMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Cinema>(m => m.Properties(p => p
                //.Number(t => t.Name(n => n.id).Type(NumberType.Integer))
                .Text(t => t.Name(n => n.name))
                .Text(t => t.Name(n => n.logo))
                .Text(t => t.Name(n => n.description))
            )
            );
        }
        public static CreateIndexDescriptor ProducerMapping(this CreateIndexDescriptor descriptor)
        {
            return descriptor.Map<Producer>(m => m.Properties(p => p
                //.Number(t => t.Name(n => n.id).Type(NumberType.Integer))
                .Text(t => t.Name(n => n.fullName))
                .Text(t => t.Name(n => n.profilePictureURL))
                .Text(t => t.Name(n => n.bio))
            )
            );
        }
    }
}
