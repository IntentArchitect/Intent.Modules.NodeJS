import { TpcPoly_BaseClassNonAbstract } from './tpc-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpc_poly_concrete_a')
export class TpcPoly_ConcreteA extends TpcPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}