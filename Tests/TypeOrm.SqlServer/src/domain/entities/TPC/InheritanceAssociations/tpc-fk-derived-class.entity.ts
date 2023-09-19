import { TpcFkBaseClass } from './tpc-fk-base-class.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpc_fk_derived_class')
export class TpcFkDerivedClass extends TpcFkBaseClass {
  @Column()
  derivedField: string;

}