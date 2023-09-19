import { TpcPoly_BaseClassNonAbstract } from './tpc-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpc_poly_concrete_b')
export class TpcPoly_ConcreteB extends TpcPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}